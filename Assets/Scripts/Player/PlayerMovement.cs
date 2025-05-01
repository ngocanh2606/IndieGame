using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GravityController gravityScript;

    //Jump
    public float jumpForce = 10f;  
    public LayerMask groundLayer;  
    public Transform[] groundCheck;
    private bool isGrounded;
    [System.NonSerialized] public bool isJumping = false;

    //Check for colliding with wall
    public Transform[] wallCheck;
    private Vector3 playerRight;
    private Vector3 playerLeft;

    //Move horizontally
    public float speed = 5f;
    [System.NonSerialized] public Vector2 moveDirection = Vector2.zero;

    //Animation
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

        playerRight = wallCheck[0].localPosition;
        playerLeft = wallCheck[1].localPosition;

    }

    void FixedUpdate()
    {
        if (gravityScript == null) return;

        CheckCollideWall();
        CheckIsGrounded();

        // Apply normal movement
        transform.Translate(moveDirection * speed * Time.fixedDeltaTime);

        ApplyGravity();
        RunAnim();
    }

    private void CheckIsGrounded()
    {
        foreach (Transform check in groundCheck)
        {
            if (Physics2D.OverlapCircle(check.position, 0.1f, groundLayer))
            {
                isGrounded = true;
                isJumping = false;
                return;
            }
        }

        isGrounded = false;
    }

    private void CheckCollideWall()
    {
        if (Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, groundLayer) && !isGrounded)
        {
            moveDirection.x = 0;
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
        if (direction.x > 0)
        {
            sprite.flipX = false;
            wallCheck[0].localPosition = playerRight;
            wallCheck[1].localPosition = playerLeft;
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
            wallCheck[0].localPosition = playerLeft;
            wallCheck[1].localPosition = playerRight;
        }
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    public void StopMovingHorizontally()
    {
        moveDirection.x = 0;
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
            isJumping = true;
            AudioManager.instance.PlayJumpSFX();
            rb.velocity = new Vector2(rb.velocity.x, 0); 
            rb.AddForce(-gravityScript.gravityDirection * jumpForce, ForceMode2D.Impulse);  // Apply the jump force in the opposite direction of gravity
        }
    }

    public void ApplyGravity()
    {
        rb.AddForce(gravityScript.gravityForce, ForceMode2D.Force);
    }
}
