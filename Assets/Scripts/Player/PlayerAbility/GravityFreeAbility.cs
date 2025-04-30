using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : MonoBehaviour
{
    [SerializeField] private float gravityFreeDuration = 5f; 
    [System.NonSerialized] public bool isGravityFree = false;

    private float originalJumpForce;
    [SerializeField] private float jumpForceMultiplier = 1.5f;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        playerMovement.jumpForce *= jumpForceMultiplier;

        yield return new WaitForSeconds(gravityFreeDuration);

        playerMovement.jumpForce = originalJumpForce;
        isGravityFree = false;
    }
}
