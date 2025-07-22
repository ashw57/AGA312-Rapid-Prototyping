using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

namespace Prototype3
{
    public class CharacterController : Singleton<CharacterController>
    {
        [System.Serializable]
        public class MouseSettings
        {
            public Vector2 Damping = new Vector2(1f, 5f);
            public Vector2 Sensitivity = new Vector2(1f, 5f);
        }
        [SerializeField] private MouseSettings MouseControl;

        private Vector2 smoothedMouse;

        public float rotationSpeed = 100f;

        private Crosshair m_Crosshair;

        [SerializeField]
        private Transform cameraPivot;

        private float verticalRotation = 0f;
        private float verticalRotationLimit = 100f; // Max up/down angle

        private Crosshair Crosshair
        {
            get
            {
                if (m_Crosshair == null)
                    m_Crosshair = GetComponentInChildren<Crosshair>();
                return m_Crosshair;
            }
        }

        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // -1 to 1

            // Rotate character horizontally ONLY from keyboard input
            transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

            // Get raw mouse Y input for vertical aiming only
            float mouseY = Input.GetAxis("Mouse Y");

            // Smooth mouse Y input
            smoothedMouse.y = Mathf.Lerp(smoothedMouse.y, mouseY, 1f / MouseControl.Damping.y);

            // Update vertical rotation and clamp
            verticalRotation -= smoothedMouse.y * MouseControl.Sensitivity.y * rotationSpeed * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

            if (cameraPivot != null)
                cameraPivot.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
          
            if (Crosshair != null)
                Crosshair.Lookheight(verticalRotation);
        }
    }
}
