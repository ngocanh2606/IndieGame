using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float constantSpeed = 5f;       // Speed of movement when sliding
    public float singleUnit = 1f;          // Distance moved on tap
    public float slideThreshold = 10f;     // Minimum distance to detect a slide

    public PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    public Button leftButton;
    public Button rightButton;

    private Vector2 startTouchPosition;
    private bool isSliding;

    void Start()
    {
        // Assign button event listeners
        leftButton.onClick.AddListener(() => OnButtonPress(Vector2.left));
        rightButton.onClick.AddListener(() => OnButtonPress(Vector2.right));
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSliding = false;
                    break;

                case TouchPhase.Moved:
                    float distance = touch.position.x - startTouchPosition.x;

                    if (Mathf.Abs(distance) > slideThreshold)
                    {
                        // Sliding detected
                        isSliding = true;
                        Vector2 direction = distance > 0 ? Vector2.right : Vector2.left;
                        playerMovement.SetMoveDirection(direction);
                    }
                    break;

                case TouchPhase.Ended:
                    playerMovement.StopMoving(); // Stop movement when touch ends

                    if (!isSliding)
                    {
                        // It's a tap, move by single unit
                        float tapDistance = touch.position.x - startTouchPosition.x;
                        Vector2 direction = tapDistance > 0 ? Vector2.right : Vector2.left;
                        playerMovement.MoveSingleUnit(direction, singleUnit);
                    }
                    break;
            }
        }
    }

    private void OnButtonPress(Vector2 direction)
    {
        // Move by a single unit in the direction of the button press
        playerMovement.MoveSingleUnit(direction, singleUnit);
    }
}
