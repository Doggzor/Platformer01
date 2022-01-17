using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private LayerMask groundLayer;
    private float gravityScaleNormal = 0f;
    private float gravityScaleFalling = 0f;
    private float dirX = 0f;
    private float jumpPressTime = -1f;
    private float timeLastGrounded = -1f;

    enum State
    {
        JUMPING,
        FALLING,
        IDLE,
        RUNNING
    }

    private State state = State.IDLE;

    [SerializeField] float acceleration = 2f;
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float jumpToleranceTime = 0.15f;
    [SerializeField] float coyoteTime = 0.07f;
    [SerializeField] float gravityFallMultiplier = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        gravityScaleNormal = rb.gravityScale;
        gravityScaleFalling = rb.gravityScale * gravityFallMultiplier;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        State debugState = state;

        dirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow)) jumpPressTime = Time.time;
        if (IsGrounded()) timeLastGrounded = Time.time;

        DetectState();
        HandleGravity();

        if (state != debugState) Debug.Log(state.ToString());
    }

    private void HandleGravity()
    {
        rb.gravityScale = state == State.FALLING ? gravityScaleFalling : gravityScaleNormal;
        if (!Input.GetKey(KeyCode.UpArrow) && state == State.JUMPING) rb.gravityScale = gravityScaleFalling * 1.66f;
    }

    private void FixedUpdate()
    {
        //Running
        rb.AddForce(new Vector2(dirX * acceleration, 0), ForceMode2D.Impulse);
        rb.velocity = new Vector2(Mathf.Min(Mathf.Abs(rb.velocity.x), speed) * dirX, rb.velocity.y);

        //Jumping
        if (Time.time - jumpPressTime <= jumpToleranceTime && Time.time - timeLastGrounded <= coyoteTime) Jump();

        
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.size, 0f, Vector2.down, 0.4f, groundLayer);
    }

    private void DetectState()
    {
        if (IsGrounded())
        {
            state = Mathf.Abs(dirX) > 0.05f ? State.RUNNING : State.IDLE;
            return;
        }
        state = rb.velocity.y >= 0.1f ? State.JUMPING : State.FALLING;
    }
    private void HandleState()
    {
        switch (state)
        {
            case State.IDLE:
                break;
            case State.RUNNING:
                break;
            case State.JUMPING:
                break;
            case State.FALLING:
                break;
            default:
                break;
        }
    }
}
