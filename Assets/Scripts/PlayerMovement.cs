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

    private GravityController gravityScript;

    private PlayerAnimation playerAnimation;
    [System.NonSerialized] public SpriteRenderer sprite;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        gravityScript = FindObjectOfType<GravityController>();

    }

    void FixedUpdate()
    {
        if (gravityScript == null) return;

        // Check if the player is grounded using a small circle overlap at the groundCheck position
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Apply normal movement
        transform.Translate(moveDirection * speed * Time.fixedDeltaTime);

        ApplyGravity();
        RunAnim();

    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
        if (direction.x > 0)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
        }
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    public void RunAnim()
    {
        if (isGrounded && (moveDirection == Vector2.zero))
        {
            playerAnimation.SetState(PlayerCharacterState.Idle);
        }
        else if (isGrounded && (moveDirection != Vector2.zero))
        {
            playerAnimation.SetState(PlayerCharacterState.Run);
        }
        else if (!isGrounded)
        {
            playerAnimation.SetState(PlayerCharacterState.Jump);
            if (rb.velocity.y > 0.1f)
            {
                playerAnimation.SetState(PlayerCharacterState.Jump);
            }
            else if (rb.velocity.y < -0.1f)
            {
                playerAnimation.SetState(PlayerCharacterState.Fall);
            }
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset the vertical velocity to avoid double jumping
            rb.AddForce(-gravityScript.gravityDirection * jumpForce, ForceMode2D.Impulse);  // Apply the jump force in the opposite direction of gravity
        }
    }

    public void ApplyGravity()
    {
        // Apply gravity force (opposite of gravity direction) based on gravity scale
        rb.AddForce(gravityScript.gravityForce, ForceMode2D.Force);  // Continuous gravity force
    }
}
