using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visuals : MonoBehaviour
{
    [SerializeField] private Transform visuals;
    [SerializeField] private LayerMask wahtIsGround;
    [SerializeField] private float verticalRotationSpeed;

    private void AlignWithSlope()
    {
        if (visuals == null)
            return;

        if (Physics.Raycast(visuals.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, wahtIsGround))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}
