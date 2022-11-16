using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotateAngle;

    private void Update()
    {
        gameObject.transform.Rotate(rotateAngle * Time.deltaTime);
    }
}
