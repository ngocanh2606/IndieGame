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

    public Vector2 gravityDirection = Vector2.down;

    [System.NonSerialized] public Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;

    private GravityController gravityScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravityScript = FindObjectOfType<GravityController>();  // This finds the GravityController script in the scene
        if (gravityScript == null)
        {
            Debug.LogError("GravityController script not found in the scene.");
        }
    }

    void Update()
    {
        // Get the gravity direction from the GravityController script
        gravityDirection = gravityScript.gravityDirection;

        // Check if the player is grounded using a small circle overlap at the groundCheck position
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Apply normal movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        //ApplyGravity(gravityDirection);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    public void Jump(Vector2 gravityDirection)
    {
        if (isGrounded)
        {
            // Apply the jump force on the Y-axis
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Apply the jump force in the opposite direction of gravity
            Vector2 jumpDirection = -gravityDirection.normalized;  // Opposite of gravity direction
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset the vertical velocity to avoid double jumping
            rb.AddForce(jumpDirection * 1, ForceMode2D.Impulse);  // Apply the jump force in the opposite direction of gravity
        }
    }

    //public void ApplyGravity(Vector2 gravityDirection)
    //{
    //    // Apply gravity force (opposite of gravity direction) based on gravity scale
    //    rb.AddForce(gravityDirection * gravityScript.gravityStrength, ForceMode2D.Force);  // Continuous gravity force
    //}
}
