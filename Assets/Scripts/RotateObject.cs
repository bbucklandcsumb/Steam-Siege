using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        float newRotationSpeed = rotationSpeed * 100;
        transform.Rotate(newRotationSpeed * Time.deltaTime * rotationVector);
    }
}
