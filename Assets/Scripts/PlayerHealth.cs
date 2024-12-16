using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;      // Player's maximum health
    private float currentHealth;        // Player's current health

    [SerializeField] private float regenerationRate = 5f;      // Amount of health regenerated per tick
    [SerializeField] private float regenerationInterval = 1f;  // Time interval between regeneration ticks (in seconds)
    private float regenerationTimer;         // Timer to track regeneration intervals

    public bool isDead = false;
    public static bool isPlayerDead = false;

    void Start()
    {
        currentHealth = maxHealth;      // Initialize current health to max health
    }

    void Update()
    {
        // Regenerate health if below max
        if (currentHealth < maxHealth && !isDead)
        {
            HandleHealthRegeneration();
        }
    }

    private void HandleHealthRegeneration()
    {
        // Increase the regeneration timer
        regenerationTimer += Time.deltaTime;

        // Regenerate health at set intervals
        if (regenerationTimer >= regenerationInterval)
        {
            currentHealth += regenerationRate;
            regenerationTimer = 0f; // Reset the timer

            // Clamp the health to not exceed max health
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            Debug.Log("Player health regenerated: " + regenerationRate + ", Current Health: " + currentHealth);
        }
    }

    public void OnTakeDamage(float damageAmount)
    {
        if (isDead) return; // Don't take damage if player is dead
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        Debug.Log("Player took damage: " + damageAmount + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;  // Prevent multiple death triggers
        isDead = true;
        isPlayerDead = true;
        //death anim here
        Debug.Log("Player has died!");
        DisablePlayerMovement();
        //trigger game over screen
    }

    private void DisablePlayerMovement()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;  
        }

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        AbilityManager abilityManager = GetComponent<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.enabled = false;
        }

        PlayerShooting playerShooting = GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.enabled = false; 
        }
    }
}
