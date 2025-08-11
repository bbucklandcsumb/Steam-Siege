using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Visuals : MonoBehaviour
{
    [SerializeField] private GameObject onDeathFx;
    [SerializeField] private float onDeathFxScale = .5f;
    [Space]
    [SerializeField] protected Transform visuals;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float verticalRotationSpeed;

    private void Update()
    {
        AlignWithSlope();
    }

    public void CreateOnDeathVFX()
    {
        GameObject newDeathVFX = Instantiate(onDeathFx, transform.position + new Vector3(0, .15f, 0), quaternion.identity);
        newDeathVFX.transform.localScale = new Vector3(onDeathFxScale, onDeathFxScale, onDeathFxScale);
    }

    private void AlignWithSlope()
    {
        if (visuals == null)
            return;

        if (Physics.Raycast(visuals.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, whatIsGround))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            visuals.rotation = Quaternion.Slerp(visuals.rotation, targetRotation, Time.deltaTime * verticalRotationSpeed);
        }
    }
}
