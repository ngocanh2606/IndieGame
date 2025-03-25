using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityChangeInterval = 3f;  // Interval at which gravity changes
    public float gravityStrength;       // Strength of gravity force (increase for faster falling)
    public Rigidbody2D playerRigidbody;       // The player's Rigidbody2D
    public Transform playerTransform;         // Player's Transform to rotate accordingly

    public Vector2 gravityDirection = Vector2.down;         // Current gravity direction

    void Start()
    {
        // Initialize with a random gravity direction
        //ChangeGravity();

        Vector2 gravityForce = Vector2.down * (9.81f * gravityStrength);
        playerRigidbody.AddForce(gravityForce, ForceMode2D.Force);

        StartCoroutine(GravityShift());
    }

    //void Update ()
    //{
    //    // Apply the gravity force in the new gravity direction continuously
    //    playerRigidbody.AddForce(gravityDirection * gravityStrength, ForceMode2D.Force);
    //}

    // Coroutine to periodically change gravity
    IEnumerator GravityShift()
    {
        while (true)
        {
            yield return new WaitForSeconds(gravityChangeInterval);
            ChangeGravity();
        }
    }


    // Function to randomly change gravity direction
    void ChangeGravity()
    {
        // Randomly choose one of the four directions (left, right, down, up)
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0: // Left
                gravityDirection = Vector2.left;
                break;
            case 1: // Right
                gravityDirection = Vector2.right;
                break;
            case 2: // Down
                gravityDirection = Vector2.down;
                break;
            case 3: // Up
                gravityDirection = Vector2.up;
                break;
        }

        // Apply gravity direction and player rotation
        ApplyGravity();
    }

    // Apply the gravity force and rotate the player accordingly
    void ApplyGravity()
    {
        // Set the gravity direction for the player
        playerRigidbody.velocity = Vector2.zero; // Reset velocity to avoid leftover movement

        //// Apply the gravity force in the new gravity direction continuously
        //playerRigidbody.AddForce(gravityDirection * gravityStrength, ForceMode2D.Force);

        // Apply gravity force in the new direction, considering the player's gravityScale
        Vector2 gravityForce = gravityDirection * (9.81f * gravityStrength);

        // Apply the gravity force to the player
        playerRigidbody.AddForce(gravityForce, ForceMode2D.Force);

        // Rotate player to face the new gravity direction
        float angle = 0f;

        if (gravityDirection == Vector2.left)
        {
            angle = -90f;  // Rotate 90 degrees to the left
        }
        else if (gravityDirection == Vector2.right)
        {
            angle = 90f;  // Rotate 90 degrees to the right
        }
        else if (gravityDirection == Vector2.down)
        {
            angle = 0f;  // Rotate 180 degrees (down)
        }
        else if (gravityDirection == Vector2.up)
        {
            angle = 180f;    // No rotation, up is the default
        }

        // Rotate the player to align with the gravity direction
        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}