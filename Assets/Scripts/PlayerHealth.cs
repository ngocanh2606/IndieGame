using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;      // Player's maximum health
    private float currentHealth;        // Player's current health

    public float regenerationRate = 5f;      // Amount of health regenerated per tick
    public float regenerationInterval = 1f;  // Time interval between regeneration ticks (in seconds)
    private float regenerationTimer;         // Timer to track regeneration intervals


    void Start()
    {
        currentHealth = maxHealth;      // Initialize current health to max health
    }

    void Update()
    {
        // Regenerate health if below max
        if (currentHealth < maxHealth)
        {
            HandleHealthRegeneration();
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("colliding");
    //     Check if the collision is with an object tagged "Enemy"
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //         Try to get the EnemyDamage component from the collided object
    //        TestDamage enemyDamage = collision.gameObject.GetComponent<TestDamage>();
    //        if (enemyDamage != null)
    //        {
    //             Apply the specific damage amount based on the enemy
    //            TakeDamage(enemyDamage.damageAmount);
    //        }
    //    }
    //}

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

    private void Die()
    {
        Debug.Log("Player has died!");
        // Add death handling logic here (e.g., disable player, trigger respawn, show game over screen)
    }

    public void OnTakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took damage: " + damageAmount + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
