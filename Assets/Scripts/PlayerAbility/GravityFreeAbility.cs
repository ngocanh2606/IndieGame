using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : UseAbility
{
    public float gravityFreeDuration = 5f; // Duration the gravity-free effect lasts
    private bool isGravityFree = false;   // To track if the ability is currently active

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get reference to Rigidbody (make sure the player has a Rigidbody)
    }

    public override void Activate()
    {
        if (!isGravityFree)
        {
            StartCoroutine(GravityFreeEffect());
        }
    }

    private IEnumerator GravityFreeEffect()
    {
        isGravityFree = true;

        // Disable gravity
        rb.useGravity = false;

        // Allow player to move freely in all directions (no gravity)
        // You can also adjust player movement logic here if needed

        Debug.Log("Gravity Free Ability Activated!");

        // Wait for the duration of the ability
        yield return new WaitForSeconds(gravityFreeDuration);

        // Re-enable gravity after the ability duration
        rb.useGravity = true;

        Debug.Log("Gravity Free Ability Deactivated!");

        isGravityFree = false;
    }
}
