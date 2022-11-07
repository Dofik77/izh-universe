using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    private void OnMouseDrag()
    {
        float XAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YAxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        
        transform.Rotate(Vector3.down, XAxisRotation);
        transform.Rotate(Vector3.right, YAxisRotation);
    }
}
