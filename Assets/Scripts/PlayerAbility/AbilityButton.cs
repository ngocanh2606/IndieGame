using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    public void OnAbilityButtonPressed()
    {
        // Trigger the Gravity Dash ability
        playerMovement.TriggerDash();

        Debug.Log("Hi");
        // Trigger other abilities if needed
        // Example: playerMovement.TriggerAnotherAbility();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.TriggerDash();
        }
    }
}
