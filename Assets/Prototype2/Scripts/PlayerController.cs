using System.Collections;
using UnityEngine;

namespace Prototype2
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb;
        private GameObject focalPoint;

        public float speed = 5.0f;
        public float threshold;
        public Vector3 resetPosition = new Vector3(0.41f, 2.32f, 0.005f);

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");

            playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void FixedUpdate()
        {
            if (transform.position.y < threshold)
            {
                transform.position = resetPosition;
                playerRb.angularVelocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
        }
        void Update()
        {
            float forwardInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 moveDirection = (focalPoint.transform.forward * forwardInput +
                                     focalPoint.transform.right * horizontalInput).normalized;

            playerRb.AddForce(moveDirection * speed);
        }


    }
}
