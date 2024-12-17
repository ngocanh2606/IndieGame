using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : MonoBehaviour
{
    [SerializeField] private float gravityFreeDuration = 5f; // Duration the gravity-free effect lasts
    [System.NonSerialized] public bool isGravityFree = false;   // To track if the ability is currently active

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get reference to Rigidbody (make sure the player has a Rigidbody)
    }

    public void Activate()
    {
        StartCoroutine(GravityFreeEffect());
    }

    private IEnumerator GravityFreeEffect()
    {
        isGravityFree = true;

        // Disable gravity
        rb.gravityScale = 0;

        // Allow player to move freely in all directions (no gravity)
        // You can also adjust player movement logic here if needed

        Debug.Log("Gravity Free Ability Activated!");

        // Wait for the duration of the ability
        yield return new WaitForSeconds(gravityFreeDuration);

        // Re-enable gravity after the ability duration
        rb.gravityScale = 3;

        Debug.Log("Gravity Free Ability Deactivated!");

        isGravityFree = false;
    }
}
