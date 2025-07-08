using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype2
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb;
        private Transform focalPoint;

        public float speed = 5.0f;
        public float threshold;
        public Vector3 resetPosition = new Vector3(0.41f, 2.32f, 0.005f);

        public bool hasPickup = false;
        public float pickupDuration = 5f;

        public GameObject projectilePrefab;
        public Transform projectileSpawnPoint;
        public float projectileForce = 10f;

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = Camera.main.transform;

            playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        void Update()
        {
           if (hasPickup && Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            } 
        }

        private void Shoot()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();

            if (projRb != null)
            {
                 projRb.AddForce(projectileSpawnPoint.forward * projectileForce, ForceMode.Impulse);
            }      

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup"))
            {          
                Destroy(other.gameObject);
                StartCoroutine(PickupCountdown());
            }
        }

        private IEnumerator PickupCountdown()
        {
            hasPickup = true;

            GetComponent<Renderer>().material.color = Color.green;

            yield return new WaitForSeconds(pickupDuration);

            hasPickup = false;

            GetComponent<Renderer>().material.color = Color.grey;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && hasPickup)
            {
                Destroy(collision.gameObject);
                hasPickup = false;
            }
        }

    }
}
