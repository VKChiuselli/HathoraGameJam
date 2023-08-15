using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 3.0f;

    private void Update()
    {
        // Calculate rotation angles for both z and y axes
        float rotationAngleZ = Mathf.Sin(Time.time) * 30; // Rotate around z-axis with a sine function
        float rotationAngleY = Time.time * 50; // Rotate around y-axis with linear function

        // Apply rotations using Lerp
        Quaternion targetRotation = Quaternion.Euler(0, rotationAngleY, rotationAngleZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
 



}
