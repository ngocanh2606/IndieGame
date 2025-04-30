using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Health values
    [SerializeField] private float maxHealth = 100f;     
    private float currentHealth;                

    //Check state
    public bool isDead = false;
    public static bool isPlayerDead = false;
    [SerializeField] private GameManager gameManager;         

    [SerializeField] private Slider healthBar;                 // Reference to the UI Slider (Health Bar)

    private PlayerAnimation playerAnimation;

    private bool isTutorial = false;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        isDead = false;
        isPlayerDead = isDead;
        currentHealth = maxHealth;  
        gameManager = FindObjectOfType<GameManager>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; 
            healthBar.value = currentHealth; 
        }

        if (TutorialManager.instance != null)
        {
            isTutorial = true;
        }
    }

    void Update()
    {
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
        currentHealth = Mathf.Max(currentHealth, 0);

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
            currentHealth = maxHealth; // Reset health for tutorial
            TutorialManager.instance.currentStep = TutorialStep.LoseRestart;
            return;
        }

        isDead = true;
        isPlayerDead = isDead;

        gameManager.Lose();
    }
}
