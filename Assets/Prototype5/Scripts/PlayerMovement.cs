using UnityEngine;

namespace Prototype5
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight = 5f;

        private Animator anim;

        private bool IsGrounded;
        private Rigidbody2D body;

        private void Awake()
        {

            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

        }

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            //Flip player sprite when moving left and right
            if (horizontalInput> 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput > -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);


            if (Input.GetKey(KeyCode.Space) && IsGrounded == true)
            {
                Jump();
            }

            //Set animator parameters
            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", IsGrounded);
        }

        private void Jump()
        {
            body.linearVelocity = new Vector2 (body.linearVelocity.x, jumpHeight * speed);
            anim.SetTrigger("Jump");
            IsGrounded = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                IsGrounded = true;
            }
        }
    }

}