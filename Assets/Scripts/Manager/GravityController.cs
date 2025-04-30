using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    //Custom Gravity values
    public float gravityStrength;
    public Vector2 gravityForce;

    //For changing directions
    public Vector2 gravityDirection = Vector2.down;
    public float gravityChangeInterval = 3f; 
    private int currentDirection = 2;

    //References
    public Rigidbody2D playerRigidbody;
    public Transform playerTransform;

    //For Tutorial
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
            int randomDirection = Random.Range(0, 4);
            if (randomDirection != currentDirection)
            {
                AudioManager.instance.PlayGravityChangeSFX();
                currentDirection = randomDirection;
            }

            switch (randomDirection)
            {
                case 0:
                    gravityDirection = Vector2.left;
                    break;
                case 1:
                    gravityDirection = Vector2.right;
                    break;
                case 2:
                    gravityDirection = Vector2.down;
                    break;
                case 3:
                    gravityDirection = Vector2.up;
                    break;
            }

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
        playerRigidbody.velocity = Vector2.zero;

        gravityForce = gravityDirection * gravityStrength;

        playerRigidbody.AddForce(gravityForce, ForceMode2D.Force);

        float angle = 0f;

        if (gravityDirection == Vector2.left)
        {
            angle = -90f;
        }
        else if (gravityDirection == Vector2.right)
        {
            angle = 90f;
        }
        else if (gravityDirection == Vector2.down)
        {
            angle = 0f;
        }
        else if (gravityDirection == Vector2.up)
        {
            angle = 180f;
        }

        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}