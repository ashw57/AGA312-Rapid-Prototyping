using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
namespace Prototype3
{
    public class CharacterController : MonoBehaviour
    {
        [System.Serializable]
        public class MouseSettings
        {
            public Vector2 Damping = new Vector2(1f,5f);
            public Vector2 Sensitivity = new Vector2(1f, 5f);
        }
        [SerializeField] private MouseSettings MouseControl;

        public Vector2 MouseInput { get; private set; }

        private Vector2 smoothedMouse;

        public float rotationSpeed = 100f;

        private Crosshair m_Crosshair;

    


        private Crosshair Crosshair 
        { get 
            { 
                if (m_Crosshair == null) 
                    m_Crosshair = GetComponentInChildren<Crosshair>();
                return m_Crosshair;
            } 
        }
      
        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

            smoothedMouse.y = Mathf.Lerp(smoothedMouse.y, MouseInput.y, 1f / MouseControl.Damping.y);
            
            Crosshair.Lookheight(smoothedMouse.y * MouseControl.Sensitivity.y);
        }
    }
}
