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

    [System.NonSerialized] public bool moveLeft;                  // Track if left button is being held
    [System.NonSerialized] public bool moveRight;                 // Track if right button is being held
    [System.NonSerialized] public bool jump = false;

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
        if (PauseManager.instance.isPaused) { return; }
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
            playerMovement.StopMoving();
        }

        if (jump)
        {
            playerMovement.Jump();
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
        if (PauseManager.instance.isPaused) { return; }

        jump = true;
    }
}
