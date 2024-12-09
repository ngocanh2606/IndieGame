using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAbility : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed at which the ability falls
    public float lifetime = 10f; // Time before the ability disappears if not collected
    private float lifetimeTimer;

    private void Start()
    {
        lifetimeTimer = lifetime; // Set the initial lifetime of the ability
    }

    private void Update()
    {
        // Make the ability fall
        Fall();

        // Decrease lifetime and check if the ability should disappear
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            Destroy(gameObject); // Destroy the ability when it goes off-screen or expires
        }
    }

    private void Fall()
    {
        // Apply falling logic (move downward over time)
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Check if the ability has fallen off-screen
        if (transform.position.y < -10f) // Adjust the value depending on your screen bounds
        {
            Destroy(gameObject);
        }
    }

}
