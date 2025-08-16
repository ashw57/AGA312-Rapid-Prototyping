using UnityEngine;

namespace Prototype5
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight = 5f;

        private bool IsGrounded;
        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();

        }

        private void Update()
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

            if (Input.GetKey(KeyCode.Space) && IsGrounded == true)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight * speed);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsGrounded = false;
            }
        }

    }

}