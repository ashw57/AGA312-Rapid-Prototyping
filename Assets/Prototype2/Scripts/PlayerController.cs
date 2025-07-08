using System.Collections;
using UnityEngine;

namespace Prototype2
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb;
        private Transform focalPoint;

        public float speed = 5.0f;
        public float threshold;
        public Vector3 resetPosition = new Vector3(0.41f, 2.32f, 0.005f);

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = Camera.main.transform;

            playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        void FixedUpdate()
        {
            if (transform.position.y < threshold)
            {
                transform.position = resetPosition;
                playerRb.angularVelocity = Vector3.zero;
                
            }

            float forwardInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 forward = Vector3.ProjectOnPlane(focalPoint.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(focalPoint.right, Vector3.up).normalized;

            Vector3 moveDirection = (forward * forwardInput + right * horizontalInput).normalized;

            playerRb.AddForce(moveDirection * speed);
        }
      


    }
}
