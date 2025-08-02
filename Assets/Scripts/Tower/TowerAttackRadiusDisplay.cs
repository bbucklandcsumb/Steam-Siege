using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TowerAttackRadiusDisplay : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private float radius;
    private int segments = 50;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // +1 to close the circle
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = FindFirstObjectByType<BuildManager>().GetAttackRadiusMat();
    }


    /*public void ShowAttackRadius(bool showRadius, float newRadius, Vector3 newCenter)
    {
        lineRenderer.enabled = showRadius;

        if (showRadius == false)
            return;

        transform.position = newCenter;
        radius = newRadius;
        CreateCircle();
    }*/

    public void CreateCircle(bool showCircle, float radius = 0)
    {
        lineRenderer.enabled = showCircle;

        if (showCircle == false)
            return;

        float angle = 0f;
        Vector3 center = transform.position;

        for (int i = 0; i < segments; i++)
        {
            // Calculate the position of each point on the circle
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x + center.x, center.y, z + center.z));
            angle += 360f / segments;
        }

        // Close the circle by connecting the last point to the first
        lineRenderer.SetPosition(segments, lineRenderer.GetPosition(0));
    }
}
