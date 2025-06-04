 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    bool isFacingRight = true;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundcheckPosition;
    public Vector2 groundcheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask wallLayer;

    [Header("Wall Movement")]
    public float wallSlideSpeed = 4f;
    bool isWallSliding;

    // Wall Jumping
bool isWallJumping;
float wallJumpDirection;
float wallJumpTime = 0.5f;
float wallJumpTimer;
public Vector2 wallJumpPower = new Vector2(5f, 10f);


void Update()
{
    GroundCheck();
    ProcessGravity();
    ProcessWallSlide();
    ProcessWallJump();

    if (!isWallJumping)
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Flip();
    }

    // Animator
    animator.SetFloat("yVelocity", rb.velocity.y);
    animator.SetFloat("magnitude", rb.velocity.magnitude);
    animator.SetBool("isWallSliding", isWallSliding);

}


    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundcheckPosition.position, groundcheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void ProcessWallSlide()
    {
        if (!isGrounded && WallCheck() && Mathf.Abs(horizontalMovement) > 0.1f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, +wallSlideSpeed));
            rb.gravityScale = 0f;

            // Debug untuk memantau wall slide
            Debug.Log($"Wall sliding! Velocity Y: {rb.velocity.y}");
        }
        else
        {
            isWallSliding = false;
        }
    }

private void ProcessWallJump()
{
    if (isWallSliding)
    {
        isWallJumping = false;
        wallJumpDirection = -transform.localScale.x;
        wallJumpTimer = wallJumpTime;

        CancelInvoke(nameof(CancelWallJump));
    }
    else if (wallJumpTimer > 0f)
    {
        wallJumpTimer += Time.deltaTime;
    }
}


private void CancelWallJump()
{
    isWallJumping = false;
}



    private void ProcessGravity()
    {
        // Jangan apply gravitasi jika sedang wall sliding
        if (isWallSliding)
            return;

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

  public void Jump(InputAction.CallbackContext context)
{
    if (jumpsRemaining > 0)
    {
        if (context.performed)
        {
            // Hold down jump button = full height
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpsRemaining--;
            animator.SetTrigger("jump");
        }
        else if (context.canceled && rb.velocity.y > 0)
        {
            // Light tap of jump button = half the height
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
            jumpsRemaining--;
            animator.SetTrigger("jump");
        }
    }

    // Wall jump
    if (context.performed && wallJumpTimer > 0f)
    {
        isWallJumping = true;
        rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); // Jump away from wall
        wallJumpTimer = 0;
        animator.SetTrigger("jump");
        
        //Force flip
if (transform.localScale.x != wallJumpDirection)
{
    isFacingRight = !isFacingRight;
    Vector3 ls = transform.localScale;
    ls.x *= -1f;
    transform.localScale = ls;
}
 
        Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); // Wall Jump = 0.5f -- Jump again = 0.6f
    }
}


    private void OnDrawGizmosSelected()
    {
        if (groundcheckPosition != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(groundcheckPosition.position, groundcheckSize);
        }

        if (wallCheckPos != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
        }
    }
}
