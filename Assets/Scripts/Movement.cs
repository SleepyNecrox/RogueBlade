﻿using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool isWallSliding;
    [SerializeField] private float WallSlideSpeed = 2f;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpForce = 16f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool canDash = true;
    private bool isDashing;

    [SerializeField]private float dashPower = 24f;
    [SerializeField]private float dashTime = 0.2f;

    [SerializeField]private float dashCD = 1f;

    [SerializeField] private Rigidbody2D rb;					
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    [SerializeField] private Collider2D StandingCollider;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField]private float crouchSpeed = 0.5f;
    private bool isCrouching;


    private void Update() //inputs w/ timers for coyote/jumpbuffer
    {
        if(isDashing)
        {
            return;
        }


        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && !isCrouching)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if (Input.GetButtonDown("Crouch"))
            isCrouching = true;
    
        else if (Input.GetButtonUp("Crouch"))
            isCrouching = false;

        Crouched();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (canDash))
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate() //checking what the player is doing
    {
        if(isDashing)
        {
            return;
            
        }


        if (!isWallJumping)
        {
         rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
       
    }
        
    private void Crouched()
    {
        if(isCrouching || Physics2D.OverlapCircle(m_CeilingCheck.position, 0.1f ,groundLayer))
        {
            StandingCollider.enabled = false;
            speed = 4f;
        }

        else
        {
            StandingCollider.enabled = true;
            speed = 8f;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
}