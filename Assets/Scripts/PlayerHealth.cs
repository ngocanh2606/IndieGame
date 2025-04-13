using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;           // Player's maximum health
    private float currentHealth;                               // Player's current health

    [SerializeField] private float regenerationRate = 5f;      // Amount of health regenerated per tick
    [SerializeField] private float regenerationInterval = 1f;  // Time interval between regeneration ticks (in seconds)
    private float regenerationTimer;                           // Timer to track regeneration intervals

    public bool isDead = false;
    public static bool isPlayerDead = false;

    [SerializeField] private GameManager gameManager;          // Reference to the GameManager script
    [SerializeField] private Slider healthBar;                 // Reference to the UI Slider (Health Bar)

    void Start()
    {
        isDead = false;  // Initialize dead state
        isPlayerDead = isDead;
        currentHealth = maxHealth;           // Initialize current health to max health
        gameManager = FindObjectOfType<GameManager>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  // Set the max value of the slider
            healthBar.value = currentHealth; // Set the current value of the slider
        }
    }

    void Update()
    {
        // Regenerate health if below max
        if (currentHealth < maxHealth && !isDead)
        {
            HandleHealthRegeneration();
        }

        // Update the health bar to reflect the current health
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
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
        if (isDead) return;
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0

        if (currentHealth <= 0)
        {
            healthBar.value = currentHealth;
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;  // Prevent multiple death triggers
        isDead = true;
        isPlayerDead = isDead;

        gameManager.Lose();  // Call Lose method from GameManager script
    }
}
