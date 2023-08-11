using UnityEngine;

public class ChairFollow : MonoBehaviour
{
    public Transform sphereTransform; // Reference to the sphere's transform

    [Header("Rotation")]
    public float rotationSpeed = 5f; // Speed at which the chair rotates towards movement direction

    private void Update()
    {
        // Calculate movement direction on the XZ plane
        Vector3 spherePosition = new Vector3(sphereTransform.position.x, transform.position.y, sphereTransform.position.z);
        Vector3 movementDirection = spherePosition - transform.position;

        // Follow the sphere's position on the XZ plane
        transform.position = spherePosition;

        // Smoothly rotate towards the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
