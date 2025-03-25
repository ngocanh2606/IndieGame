using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : MonoBehaviour
{
    [SerializeField] private float gravityFreeDuration = 5f;    // Duration the gravity-free effect lasts
    [System.NonSerialized] public bool isGravityFree = false;   // To track if the ability is currently active

    private float originalJumpForce;
    [SerializeField] private float jumpForceMultiplier = 1.5f;
    private PlayerMovement playerMovement;  // Reference to PlayerMovement script

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Ensure to get the reference to the PlayerMovement script
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found on this object.");
        }
        originalJumpForce = playerMovement.jumpForce;
    }

    public void Activate()
    {
        if (playerMovement != null)
        {
            StartCoroutine(GravityFreeEffect());
        }
    }

    private IEnumerator GravityFreeEffect()
    {
        isGravityFree = true;

        // Increase the jump force to simulate higher jumps
        playerMovement.jumpForce *= jumpForceMultiplier;  // Increase the jump force

        // Wait for the duration of the ability
        yield return new WaitForSeconds(gravityFreeDuration);

        // Restore the jump force to its original value
        playerMovement.jumpForce = originalJumpForce;

        isGravityFree = false;
    }
}
