using UnityEngine;

namespace Prototype5
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private float wallJumpCooldown;

        private Animator anim;
        private BoxCollider2D boxCollider;
        private Rigidbody2D body;       
        private float horizontalInput;

        private void Awake()
        {

            body = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();

        }

        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");

            //Flip player sprite when moving left and right
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);


            //Set animator parameters
            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", IsGrounded());

            //Wall jump logic
            if (wallJumpCooldown < 0.2f)
            {

                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

                if (OnWall() && !IsGrounded())
                {
                    body.gravityScale = 2;
                    body.linearVelocity = Vector2.zero;
                }
                else
                    body.gravityScale = 3;

                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();
                }
            }
            else
                wallJumpCooldown += Time.deltaTime;

        }

        private void Jump()
        {
            if (IsGrounded())
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                anim.SetTrigger("Jump");
            }
            else if(OnWall() && !IsGrounded())
            {
                if(horizontalInput == 0)
                {
                    anim.SetTrigger("Climb");
                    body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                    body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 4, 7);

                wallJumpCooldown = 0;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
        }

        private bool IsGrounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            return raycastHit.collider != null; 
        }

        private bool OnWall()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
            return raycastHit.collider != null; 
        }
    }

}