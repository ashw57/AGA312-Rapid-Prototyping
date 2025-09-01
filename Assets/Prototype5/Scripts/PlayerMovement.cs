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

        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float lowJumpMultiplier = 2f;
        [SerializeField] private float wallJumpResetTime = 0.2f;

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
            if (wallJumpCooldown > wallJumpResetTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }

                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

                if (OnWall() && !IsGrounded())
                {
                    body.gravityScale = 1;
                    body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(body.linearVelocity.y, -2f));
                }
                else
                {
                    body.gravityScale = 3;
                }                    

            }
            else
                wallJumpCooldown += Time.deltaTime;

            if (body.linearVelocity.y < 0)
                body.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            else if (body.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
                body.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }

        private void Jump()
        {
            if (IsGrounded())
            {
                wallJumpCooldown = wallJumpResetTime;

                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                anim.SetTrigger("Jump");
            }
            else if (OnWall() && !IsGrounded())
            {
                float jumpDirection = -Mathf.Sign(transform.localScale.x);

                body.linearVelocity = new Vector2(jumpDirection * 6f, 7f);
                wallJumpCooldown = 0;

                transform.localScale = new Vector3(jumpDirection, transform.localScale.y, transform.localScale.z);
                anim.SetTrigger("Jump");
            }
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