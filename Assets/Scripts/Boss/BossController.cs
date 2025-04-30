using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public Transform player;
    [SerializeField] private GameObject bossGameObject;

    private int lastPhase = 1;
    private int currentPhase;


    // Health configs
    [SerializeField] private Slider healthBar;  // Reference to the boss's health bar slider
    public float maxHealth = 100f;           // The maximum health of the boss
    [System.NonSerialized] public float currentHealth = 100f;   
    public bool isDead = false;

    //Health regeneration
    [SerializeField] private float regenerationRate = 5f;      // Amount of health regenerated per tick
    [SerializeField] private float regenerationInterval = 1f;  // Time interval between regeneration ticks (in seconds)
    private float regenerationTimer;

    // Shooting pattern configurations
    private IShootPattern currentShootPattern;                 // The current shooting pattern
    [SerializeField] private float patternChangeInterval = 2f; // Fire rate time between shots
    private float lastPatternChangeTime;                       // Time of the last pattern change
    private float nextShootTime;
    [SerializeField] private float fireCooldownTime = 0.5f;        // Base fire rate (time between shots)

    // Bullet configs
    [SerializeField] private GameObject projectilePrefab;      // The projectile to shoot
    [SerializeField] private Transform shootPoint;             // The point from which the boss shoots
    private Projectile bossBullet;
    private float initialDamage = 0f;
    [SerializeField] private float damageMultiplier = 1.5f;

    private float angleFromPlayer = 0f; // Angle for the shooting pattern. when set to 0, it shoots to right

    //Tutorial
    private bool isTutorial = false;

    private void Start()
    {
        
        isDead = false;
        bossBullet = projectilePrefab.GetComponent<Projectile>(); 
        gameManager = FindObjectOfType<GameManager>();

        if (projectilePrefab != null)
        {
            bossBullet = projectilePrefab.GetComponent<Projectile>(); // Make sure the projectile prefab has a Projectile script
        }

        initialDamage = bossBullet.damage;

        currentHealth = maxHealth;                // Initialize health
        lastPatternChangeTime = Time.time;        // Set the initial pattern change time
        nextShootTime = Time.time;                // Initialize the shoot timer
        SetRandomShootPattern();                  // Initialize the first pattern

        currentPhase = lastPhase;

        // Initialize the health bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  // Set the max value of the slider
            healthBar.value = currentHealth; // Set the current value of the slider
        }

        if (TutorialManager.instance != null)
        {
            isTutorial = true;
            maxHealth = 50;
            currentHealth = maxHealth;
            currentShootPattern = new SpiralShootPattern();
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;  // Set the max value of the slider
                healthBar.value = currentHealth; // Set the current value of the slider
            }
        }
    }

    private void Update()
    {
        if (PauseManager.instance.isPaused) { return; }

        if (isTutorial)
        {
            if(TutorialManager.instance.currentStep != TutorialStep.KillBoss)
            {
                return;
            }
        }

        if (!PlayerHealth.isPlayerDead && !isDead)
        {
            // Change the shooting pattern at regular intervals
            if (Time.time - lastPatternChangeTime >= patternChangeInterval)
            {
                SetRandomShootPattern();           // Randomly change shooting pattern
                lastPatternChangeTime = Time.time; // Update the last pattern change time
            }

            // Fire rate management
            if (Time.time >= nextShootTime)
            {
                // Increase fire rate if the boss is in a higher phase
                if (currentPhase != lastPhase)
                {
                    lastPhase = currentPhase;
                    fireCooldownTime -= 0.1f;
                }

                switch (currentShootPattern)
                {
                    //Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab);
                    case SpreadShootPattern _:
                        currentShootPattern.Shoot(shootPoint.position, angleFromPlayer - 90, 20, 5, projectilePrefab);
                        break;
                   
                    case SpiralShootPattern _:
                        currentShootPattern.Shoot(shootPoint.position, angleFromPlayer - 90, 30, 3, projectilePrefab);

                        break;
                   
                    case CircularShootPattern _:
                        currentShootPattern.Shoot(shootPoint.position, 0, 0, 10, projectilePrefab);
                        break;
                }

                nextShootTime = Time.time + fireCooldownTime;
            }
        }

        if (currentHealth < maxHealth && !isDead)
        {
            HandleHealthRegeneration();
        }

        // Update the health bar
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

            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    // Randomly choose a shooting pattern, considering the health of the boss
    private void SetRandomShootPattern()
    {
        // Calculate shoot angle from player's position
        Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;
        angleFromPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        float healthPercentage = currentHealth / maxHealth;

        float roll = Random.Range(0f, 1f);

        // Select shooting pattern based on health percentage and random roll
        if (isTutorial)
        {
            currentShootPattern = new SpiralShootPattern();
            return;
        }

        if (healthPercentage > 0.7f)
        {
            currentShootPattern = roll < 0.7f ? (IShootPattern)new SpreadShootPattern() : new SpiralShootPattern();
        }
        else if (healthPercentage > 0.3f)
        {
            currentPhase = 2;
            bossBullet.damage = initialDamage * damageMultiplier;
            if (roll < 0.5f)
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
            currentPhase = 3;
            bossBullet.damage = initialDamage * damageMultiplier * damageMultiplier;
            currentShootPattern = roll < 0.5f ? (IShootPattern)new SpiralShootPattern() : new CircularShootPattern();
        }
    }

    // Method to take damage and update the health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0

        if (currentHealth <= 0)
        {
            healthBar.value = currentHealth;
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; 
        if (isTutorial)
        {
            isTutorial = false;
        }

        isDead = true;

        gameManager.Win();
        Destroy(bossGameObject);
    }
}
