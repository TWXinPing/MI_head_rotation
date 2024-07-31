using UnityEngine;

public class AlignSphereToHead : MonoBehaviour
{
    public Transform headTransform; // Reference to the head (camera) transform
    public float distanceFromHead = 2.0f; // Fixed distance from the head

    void Update()
    {
        if (headTransform != null)
        {
            // Calculate the new position
            Vector3 newPosition = headTransform.position + headTransform.forward * distanceFromHead;

            // Set the position of the sphere
            transform.position = newPosition;

            // Optional: Align the sphere to always face the head
            transform.LookAt(headTransform);
        }
    }
}
