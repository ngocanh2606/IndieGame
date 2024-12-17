using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;      // The projectile to shoot
    [SerializeField] private Transform shootPoint;             // The point from which the boss shoots
    [SerializeField] private float maxHealth = 100f;           // The maximum health of the boss
    private float currentHealth;                               // The current health of the boss

    private IShootPattern currentShootPattern;                 // The current shooting pattern

    // Shooting pattern configurations
    [SerializeField] private float patternChangeInterval = 2f; // Fire rate ime between shots)
    private float lastPatternChangeTime;                       // Time of the last pattern change

    [SerializeField] private float baseFireRate = 0.5f;        // Base fire rate (time between shots)
    private float nextShootTime;

    private void Start()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        currentHealth = maxHealth;                // Initialize health
        lastPatternChangeTime = Time.time;        // Set the initial pattern change time
        nextShootTime = Time.time;                // Initialize the shoot timer
        SetRandomShootPattern();                  // Initialize the first pattern
    }

    private void Update()
    {
        if (!PlayerHealth.isPlayerDead)
        {
            // Change the shooting pattern at regular interval
            if (Time.time - lastPatternChangeTime >= patternChangeInterval)
            {
                SetRandomShootPattern();           // Randomly change shooting pattern
                lastPatternChangeTime = Time.time; // Update the last pattern change time
            }

            //fire rate
            if (Time.time >= nextShootTime)
            {
                // Shoot the current pattern
                currentShootPattern.Shoot(shootPoint.position, 0f, 30f, 5, projectilePrefab);
                nextShootTime = Time.time + baseFireRate;
            }
        }
    }

    // Randomly choose a shooting pattern, considering the health of the boss
    private void SetRandomShootPattern()
    {
        // Calculate the health percentage of the boss
        float healthPercentage = currentHealth / maxHealth;

        // Generate a random value between 0 and 1 to use for probabilistic selection
        float roll = Random.Range(0f, 1f);

        // Select shooting pattern based on health percentage and random roll
        if (healthPercentage > 0.6f)
        {
            // Higher chance of selecting SpreadShootPattern at higher health
            currentShootPattern = roll < 0.7f ? (IShootPattern)new SpreadShootPattern() : new SpiralShootPattern();
        }
        else if (healthPercentage > 0.3f)
        {
            // Allow a chance to roll all patterns
            if (roll < 0.5f)  // 50% chance for SpreadShootPattern
            {
                currentShootPattern = (IShootPattern)new SpreadShootPattern();
            }
            else
            {
                currentShootPattern = roll < 0.75f ? (IShootPattern)new SpiralShootPattern() : new CircularShootPattern();
            }
        }
        else
        {
            baseFireRate = 0.3f; // Faster fire rate
            currentShootPattern = roll < 0.5f ? (IShootPattern)new SpiralShootPattern() : new CircularShootPattern();
        }

        Debug.Log("Pattern Changed to: " + currentShootPattern.GetType().Name);
    }

    // Method to take damage and update the health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        Debug.Log("Boss health: " + currentHealth);
    }
}
