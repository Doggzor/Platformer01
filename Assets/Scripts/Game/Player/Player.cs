using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeon
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private new BoxCollider2D collider;
        private LayerMask groundLayer;
        private Animator animator;

        private float gravityScaleNormal = 0f;
        private float gravityScaleFalling = 0f;
        private float timeLastGrounded = -1f;
        private float jumpPressTime = -1f;
        private float dirX = 0f;

        enum State
        {
            JUMPING,
            FALLING,
            IDLE,
            RUNNING,
            DEAD
        }

        private State state = State.IDLE;

        [SerializeField] float acceleration = 2f;
        [SerializeField] float speed = 12f;
        [SerializeField] float jumpForce = 15f;
        [SerializeField] float jumpToleranceTime = 0.15f;
        [SerializeField] float coyoteTime = 0.07f;
        [SerializeField] float gravityFallMultiplier = 1.5f;

        [Space]
        [SerializeField] PlayerSkinPrefab skin = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            groundLayer = LayerMask.GetMask("Ground");
            gravityScaleNormal = rb.gravityScale;
            gravityScaleFalling = rb.gravityScale * gravityFallMultiplier;
            //ApplySkin();
        }
        private void Start()
        {

        }
        private void Update()
        {
            State debugState = state;

            if (state != State.DEAD)
                HandleInput();

            if (HasFacingDirectionChanged())
                Flip();
            if (IsGrounded())
                timeLastGrounded = Time.time;

            state = DetectState();
            HandleGravity();

            if (state != debugState)
            {
                Debug.Log(state.ToString());
                if (state == State.FALLING) Debug.Log("Height reached: " + rb.position.y);
            }

            HandleAnimations();
        }
        private void FixedUpdate()
        {
            if (state != State.DEAD)
            {
                HorizontalMovement();
                Jumping();
            }
        }

        private void Jumping()
        {
            if (Time.time - jumpPressTime <= jumpToleranceTime &&
                            Time.time - timeLastGrounded <= coyoteTime)
                Jump();
        }

        private void HorizontalMovement()
        {
            rb.AddForce(new Vector2(dirX * acceleration, 0), ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Min(Mathf.Abs(rb.velocity.x), speed) * dirX, rb.velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDanger>(out _))
            {
                Debug.Log("DEAD");
                StartCoroutine(Co_TriggerDeath());
            }
            else if (collision.TryGetComponent(out IPickable obj))
            {
                obj.OnPickUp();
            }

        }
        private IEnumerator Co_TriggerDeath()
        {
            state = State.DEAD;
            collider.enabled = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = gravityScaleNormal;
            rb.AddForce(new Vector2(Random.Range(-4f, 4f), 10f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void Flip()
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        private bool HasFacingDirectionChanged()
        {
            return Mathf.Abs(transform.localScale.x - dirX) > 1.01f;
        }

        private void HandleInput()
            {
                dirX = Input.GetAxisRaw("Horizontal");
                if (Input.GetKeyDown(KeyCode.UpArrow)) jumpPressTime = Time.time;
            }

        private void HandleGravity()
        {
            rb.gravityScale = state switch
            {
                State.FALLING => gravityScaleFalling,
                State.JUMPING => Input.GetKeyUp(KeyCode.UpArrow) ? gravityScaleFalling * 1.66f : rb.gravityScale,
                _ => gravityScaleNormal
            };
        }

        private void Jump()
        {
            //Reset some values before performing the actual jump
            jumpPressTime = -1f; //Important for consistent jump heights
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = gravityScaleNormal;
            //Actual jump
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        }

        private bool IsGrounded()
        {
            return Physics2D.BoxCast(collider.bounds.center, collider.size, 0f, Vector2.down, 0.01f, groundLayer);
        }

        private State DetectState()
        {
            if (state == State.DEAD)
                return state;

            if (IsGrounded())
                return Mathf.Abs(dirX) > Mathf.Epsilon ? State.RUNNING : State.IDLE;

            if (rb.velocity.y > 0f)
                return State.JUMPING;

            if (rb.velocity.y < 0f)
                return State.FALLING;

            return state;
        }
        private void HandleAnimations()
        {
            switch (state)
            {
                case State.IDLE:
                    animator.Play("Idle");
                    break;
                case State.RUNNING:
                    animator.Play("Run");
                    break;
                case State.JUMPING:
                    animator.Play("Jump");
                    break;
                case State.FALLING:
                    animator.Play("Falling");
                    break;
                case State.DEAD:
                    //NEED TO IMPLEMENT ANIMATION animator.Play("Death");
                    break;
                default:
                    break;
            }
        }

        public void ApplySkin()
        {
            var sprite = transform.Find("Sprite");
            var body = sprite.transform.Find("Body");
            var head = body.transform.Find("Head");
            var eye_front = head.transform.Find("Eye Front");
            var eye_back = head.Find("Eye Back");
            var arm_front = body.transform.Find("Arm Front");
            var arm_back = body.transform.Find("Arm Back");
            var tail = body.transform.Find("Tail");
            var leg_front = sprite.transform.Find("Leg Front");
            var leg_back = sprite.transform.Find("Leg Back");

            head.GetComponent<SpriteRenderer>().sprite = skin.head;
            eye_front.GetComponent<SpriteRenderer>().sprite = skin.eye_front;
            eye_back.GetComponent<SpriteRenderer>().sprite = skin.eye_back;
            body.GetComponent<SpriteRenderer>().sprite = skin.body;
            arm_front.GetComponent<SpriteRenderer>().sprite = skin.arm_front;
            arm_back.GetComponent<SpriteRenderer>().sprite = skin.arm_back;
            tail.GetComponent<SpriteRenderer>().sprite = skin.tail;
            leg_front.GetComponent<SpriteRenderer>().sprite = skin.leg_front;
            leg_back.GetComponent<SpriteRenderer>().sprite = skin.leg_back;
        }
    }
}
