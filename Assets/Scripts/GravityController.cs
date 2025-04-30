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
    public Vector2 gravityForce;

    private int currentDirection = 2;

    private bool isTutorial;
    [System.NonSerialized] public int countDirection = 0;

    void Start()
    {

        gravityForce = Vector2.down * gravityStrength;
        playerRigidbody.AddForce(gravityForce, ForceMode2D.Force);

        if (TutorialManager.instance == null)
        {
            StartCoroutine(GravityShift());
            isTutorial = false;
        }
        else
        {
            isTutorial = true;
            gravityChangeInterval = 5f;
        }
    }

    public void StartGravityShift()
    {

        StartCoroutine(GravityShift());

    }

    // Coroutine to periodically change gravity
    private IEnumerator GravityShift()
    {
        if (PauseManager.instance.isPaused) { yield break; }
        while (true)
        {
            //if (isTutorial && currentDirection == 4 && )
            //{
            //    yield break;
            //}
                
            yield return new WaitForSeconds(gravityChangeInterval);

            if (isTutorial && currentDirection < 4)
            {
                ChangeGravityInTutorial();
            }
            else if (isTutorial && currentDirection == 4)
            {
                yield break;
            }
            else if (isTutorial && (int)TutorialManager.instance.currentStep > 11)
            {
                ChangeGravity();
            }
            else if (!isTutorial)
            {
                ChangeGravity();
            }
        }

        // Function to randomly change gravity direction
        void ChangeGravity()
        {
            gravityChangeInterval = 20f;
            // Randomly choose one of the four directions (left, right, down, up)
            int randomDirection = Random.Range(0, 4);
            if (randomDirection != currentDirection)
            {
                AudioManager.instance.PlayGravityChangeSFX();
                currentDirection = randomDirection;
            }

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
            if (playerRigidbody != null)
            {
                ApplyGravity();
            }
        }

        void ChangeGravityInTutorial()
        {
            if (countDirection > 3) return;
            AudioManager.instance.PlayGravityChangeSFX();

            switch (countDirection)
            {
                case 0:
                    gravityDirection = Vector2.right;
                    break;
                case 1:
                    gravityDirection = Vector2.up;
                    break;
                case 2:
                    gravityDirection = Vector2.left;
                    break;
                case 3:
                    gravityDirection = Vector2.down;
                    TutorialManager.instance.AdvanceToNextStep();
                    break;
            }


            // Apply gravity direction and player rotation
            if (playerRigidbody != null)
            {
                ApplyGravity();
            }

            if (countDirection < 4)
            {
                countDirection++;
            }
        }
    }
    // Apply the gravity force and rotate the player accordingly
    void ApplyGravity()
    {
        playerRigidbody.velocity = Vector2.zero; // Reset velocity to avoid leftover movement

        // Apply gravity force in the new direction, considering the player's gravityScale
        gravityForce = gravityDirection * gravityStrength;

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