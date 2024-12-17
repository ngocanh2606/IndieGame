using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float constantSpeed = 5f;        // Speed of movement when holding buttons
    public PlayerMovement playerMovement;   // Reference to the PlayerMovement script
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;               // The jump button

    private bool moveLeft;    // Track if left button is being held
    private bool moveRight;   // Track if right button is being held

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
    }

    private void AddEventTrigger(GameObject obj, System.Action<BaseEventData> action, EventTriggerType triggerType)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = triggerType };
        entry.callback.AddListener((data) => action.Invoke(data));
        trigger.triggers.Add(entry);
    }

    private void OnLeftButtonDown(BaseEventData data)
    {
        moveLeft = true;
    }

    private void OnRightButtonDown(BaseEventData data)
    {
        moveRight = true;
    }

    private void OnButtonUp(BaseEventData data)
    {
        moveLeft = false;
        moveRight = false;
    }

    // Jump triggered on PointerDown, as soon as the button is pressed
    private void OnJumpButtonPressed(BaseEventData data)
    {
        // Trigger jump action in PlayerMovement
        playerMovement.Jump();
    }
}
