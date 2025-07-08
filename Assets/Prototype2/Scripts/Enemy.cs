using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype2
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 5.0f;
        public int scoreValue = 10;

        private Rigidbody enemyRb;
        private GameManager gameManager;

        private Vector3 moveDirection;

        [System.Obsolete]
        void Start()
        {
            enemyRb = GetComponent<Rigidbody>();
            gameManager = FindObjectOfType<GameManager>();

            // Generate a valid random direction
            do
            {
                float randomX = Random.Range(-1f, 1f);
                float randomZ = Random.Range(-1f, 1f);
                moveDirection = new Vector3(randomX, 0, randomZ);
            } while (moveDirection.magnitude < 0.1f);

            moveDirection.Normalize();
        }

        void FixedUpdate()
        {
            // Move the enemy in a random direction
            enemyRb.AddForce(moveDirection * speed, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                gameManager.UpdateScore(scoreValue);
                Destroy(gameObject);
            }
            else
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 normal = contact.normal;
                moveDirection = Vector3.Reflect(moveDirection, normal).normalized;
            }

        }

        void Update()
        {
            // Enemy Respawn
            if (transform.position.y < -10)
            {
                gameManager.UpdateScore(scoreValue);
                Destroy(gameObject);
            }
        }

    }
}
