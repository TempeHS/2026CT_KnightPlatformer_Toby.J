using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField]
    private InputAction jumpAction;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 12f;
    public float deceleration = 10f;

    [Header("Jumping")]
    public float jumpingPower = 16f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Dashing")]
    private bool isDashing;
    private float dashSpeed = 24f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;


    private float moveInput;
    private float currentVelocityX;
    private bool isGrounded;
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;




    void Update()
    {
        if(isDashing)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
    
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    
        
    



        Flip();
    }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        var actualGravity = Mathf.Abs(Physics2D.gravity.y) * rb.gravityScale;
        float jumpVelocity = Mathf.Sqrt(2 * jumpingPower * actualGravity);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
    }

    private void OnEnable()
    {
        jumpAction.performed += HandleJumpInput;
    }

        private void OnDisable()
    {
        jumpAction.performed -= HandleJumpInput;
    }




   private void FixedUpdate()
    {
        if (isDashing)
            return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        float targetSpeed = moveInput * moveSpeed;

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            
            currentVelocityX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            
            currentVelocityX = Mathf.Lerp(rb.linearVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = new Vector2(currentVelocityX, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        tr.emitting = true;
        rb.gravityScale = 0f;   
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0f);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        tr.emitting = false;
        isDashing = false;

    }

}