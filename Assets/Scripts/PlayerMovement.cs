using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;           // Movement speed
    public float jumpForce = 10f;      // Jump force
    public LayerMask groundLayer;      // Ground layer for checking if player is grounded
    public Transform groundCheck;      // Position to check if player is grounded
    private bool isGrounded;           // Whether the player is on the ground

    [System.NonSerialized] public Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
    }

    void Update()
    {
        // Check if the player is grounded using a small circle overlap at the groundCheck position
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Apply normal movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Handle jump
        if (isGrounded && Input.GetButtonDown("Jump"))  // Replace "Jump" with your jump button if necessary
        {
            Jump();
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            // Apply the jump force on the Y-axis
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
