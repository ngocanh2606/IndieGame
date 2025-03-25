using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float constantSpeed = 5f;        // Speed of movement when holding buttons
    public PlayerMovement playerMovement;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    private bool moveLeft;                  // Track if left button is being held
    private bool moveRight;                 // Track if right button is being held
    private bool jump;

    void Start()
    {
        // Add event listeners for button hold functionality
        AddEventTrigger(leftButton.gameObject, OnLeftButtonDown, EventTriggerType.PointerDown);
        AddEventTrigger(leftButton.gameObject, OnButtonUp, EventTriggerType.PointerUp);

        AddEventTrigger(rightButton.gameObject, OnRightButtonDown, EventTriggerType.PointerDown);
        AddEventTrigger(rightButton.gameObject, OnButtonUp, EventTriggerType.PointerUp);

        // Add event listener for the jump button using PointerDown
        AddEventTrigger(jumpButton.gameObject, OnJumpButtonPressed, EventTriggerType.PointerDown);
    }

    void Update()
    {
        // Continuous movement when holding buttons
        if (moveLeft)
        {
            playerMovement.SetMoveDirection(Vector2.left);
        }
        else if (moveRight)
        {
            playerMovement.SetMoveDirection(Vector2.right);
        }
        else
        {
            playerMovement.StopMoving(); // Stop moving when neither button is held
        }

        if (jump)
        {
            playerMovement.Jump(playerMovement.gravityDirection);
            jump=false;
        }
    }

    // Helper method to add button event listeners
    private void AddEventTrigger(GameObject obj, System.Action<BaseEventData> action, EventTriggerType triggerType)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = triggerType };
        entry.callback.AddListener((data) => action.Invoke(data));
        trigger.triggers.Add(entry);
    }

    // Handle left button press
    private void OnLeftButtonDown(BaseEventData data)
    {
        moveLeft = true;
    }

    // Handle right button press
    private void OnRightButtonDown(BaseEventData data)
    {
        moveRight = true;
    }

    // Handle button release (stop movement)
    private void OnButtonUp(BaseEventData data)
    {
        moveLeft = false;
        moveRight = false;
    }

    // Handle jump button press
    private void OnJumpButtonPressed(BaseEventData data)
    {
        jump= true;
    }
}
