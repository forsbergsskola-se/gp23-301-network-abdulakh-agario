using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;  // The player's transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset;             // Offset of the camera relative to the player

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}