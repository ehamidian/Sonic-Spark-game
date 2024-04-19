using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset = new(17, 20, -5); // Adjust the offset as needed

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Set the camera's position to follow the player with the specified offset
            transform.position = playerTransform.position + offset;
        }
    }
}
