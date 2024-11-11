using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed for sliding
    private Vector2 moveDirection = Vector2.zero;

    //Gravity Dash
    public float dashSpeed = 50f;     // Boosted speed during dash
    public float dashDuration = 0.5f; // Duration of the dash in seconds
    public float dashCooldown = 1f;   // Cooldown between dashes in seconds

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector2 dashDirection;    // Direction for the dash

    void Update()
    {
        if (isDashing)
        {
            HandleDash();
        }
        else
        {
            HandleMovement();
        }

        // Cooldown countdown
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        // Apply normal movement
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void TriggerDash()
    {
        Debug.Log("Is Dashing");
        if (dashCooldownTimer <= 0f && moveDirection != Vector2.zero)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        // Set the dash direction based on the current move direction
        dashDirection = moveDirection.normalized;

        HandleDash();
    }

    private void HandleDash()
    {
        // Apply dash movement
        transform.Translate(dashDirection * dashSpeed * Time.deltaTime);

        // Countdown dash duration
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0f)
        {
            isDashing = false;
        }
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }
}
