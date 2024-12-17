using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 100f;       // Speed of the dash
    [SerializeField] private float dashDuration = 1f;      // Duration of the dash
    private Rigidbody2D rb;               // Rigidbody2D of the player
    [System.NonSerialized] public bool isDashing = false; // To track if the player is dashing

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
    }

    public void Activate(Vector2 dashDirection)
    {
        // Start the dash coroutine and pass the dash direction as a parameter
        StartCoroutine(Dash(dashDirection));
    }

    private IEnumerator Dash(Vector2 dashDirection)
    {
        // Mark the player as dashing
        isDashing = true;

        // Set the player's velocity to the dash direction * dash speed
        rb.velocity = dashDirection * dashSpeed;

        // Wait for the duration of the dash
        yield return new WaitForSeconds(dashDuration);

        // Reset the player's velocity after the dash is complete
        rb.velocity = Vector2.zero; // Using Vector2 because it's a 2D game

        // Mark the player as no longer dashing
        isDashing = false;
    }
}
