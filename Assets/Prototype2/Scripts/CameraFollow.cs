using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype2
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player;               // The player to follow
        public Vector3 offset = new Vector3(0, 5, -7);  // Offset from the player
        public float smoothSpeed = 0.125f; // Smoothness of the follow
        public float mouseSensitivity = 2f;
        public float verticalClamp = 80f;

        private float rotationX = 20f;
        private float rotationY = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void LateUpdate()
        {
            if (player == null) return;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationY += mouseX;
            rotationX -= mouseY;

            rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

            Vector3 rotatedOffset = rotation * offset;

            // Calculate the desired position
            Vector3 desiredPosition = player.position + rotatedOffset;

            // Smooth position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Always look at the player
            transform.LookAt(player);
        }
    }
}
