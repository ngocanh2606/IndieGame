using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject[] abilityPrefab;     // Ability prefabs to spawn
    public float spawnRate;                // Rate at which abilities spawn (in seconds)
    public float spawnHeightOffset = 10f;  // Height from which abilities will fall
    public float horizontalPadding = 4.2f; // Padding to prevent spawning too close to screen edges
    private Camera mainCamera;             // Reference to the main camera

    private PlayerHealth playerHealth;     // Reference to the player's health
    private BossController bossController; // Reference to the boss controller

    private void Start()
    {
        mainCamera = Camera.main;

        // Get player and boss references
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        GameObject boss = GameObject.FindGameObjectWithTag("Enemy");
        if (boss != null)
        {
            bossController = boss.GetComponent<BossController>();
        }

        // Start spawning abilities if both player and boss are alive
        if (playerHealth != null && bossController != null)
        {
            if (!playerHealth.isDead && !bossController.isDead)
            {
                InvokeRepeating("SpawnAbility", 0f, spawnRate);
            }
        }
    }

    private void Update()
    {
        // Stop spawning if player or boss is dead
        if (playerHealth.isDead || bossController.isDead)
        {
            CancelInvoke("SpawnAbility");
        }
        else if (!IsInvoking("SpawnAbility"))
        {
            InvokeRepeating("SpawnAbility", 0f, spawnRate);
        }
    }

    // Spawn ability at a random position above the screen
    private void SpawnAbility()
    {
        // Calculate screen width in world space
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize * 2f;

        float randomX = Random.Range(-screenWidth / 2 + horizontalPadding, screenWidth / 2 - horizontalPadding);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeightOffset, 0f); 
        int randomIndex = Random.Range(0, abilityPrefab.Length);

        // Instantiate the ability at the chosen position
        Instantiate(abilityPrefab[randomIndex], spawnPosition, Quaternion.identity);
    }
}
