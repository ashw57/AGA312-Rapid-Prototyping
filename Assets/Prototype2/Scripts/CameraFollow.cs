using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // The player to follow
    public Vector3 offset = new Vector3(0, 5, -7);  // Offset from the player
    public float smoothSpeed = 0.125f;     // Smoothness of the follow

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the desired position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between current and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the position
        transform.position = smoothedPosition;

        // Look at the player (optional)
        transform.LookAt(player);
    }
}
