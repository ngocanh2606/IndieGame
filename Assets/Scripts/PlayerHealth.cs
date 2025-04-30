using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;           // Player's maximum health
    private float currentHealth;                               // Player's current health

    public bool isDead = false;
    public static bool isPlayerDead = false;

    [SerializeField] private GameManager gameManager;          // Reference to the GameManager script
    [SerializeField] private Slider healthBar;                 // Reference to the UI Slider (Health Bar)

    private PlayerAnimation playerAnimation;

    private bool isTutorial = false;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

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

        if (TutorialManager.instance != null)
        {
            isTutorial = true;
        }
    }

    void Update()
    {
        // Update the health bar to reflect the current health
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    public void OnTakeDamage(float damageAmount)
    {
        if (isDead) return;

        playerAnimation.SetState(PlayerCharacterState.TakeDamage);
        AudioManager.instance.PlayHitSFX();
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
        if (isTutorial)
        {
            currentHealth = maxHealth; // Reset health for tutorial
            TutorialManager.instance.currentStep = TutorialStep.LoseRestart;
            return;
        }

        isDead = true;
        isPlayerDead = isDead;

        gameManager.Lose();  // Call Lose method from GameManager script
    }
}
