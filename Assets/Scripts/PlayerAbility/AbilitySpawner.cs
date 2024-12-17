using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject[] abilityPrefab; // Reference to the ability prefab
    public float spawnRate; // Rate at which abilities spawn (in seconds)

    public float spawnHeightOffset = 10f; // The height from which it will fall (in world space)
    public float horizontalPadding = 4.2f; // Horizontal padding to prevent spawning too close to screen edges
    private Camera mainCamera;

    private PlayerHealth playerHealth;

    private void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (playerHealth != null && !playerHealth.isDead)
        {
            InvokeRepeating("SpawnAbility", 0f, spawnRate);
        }

        float test = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        Debug.Log("ScreenWidth: " + test);
    }

    private void Update()
    {
        // If the player dies, stop the spawning
        if (playerHealth != null && playerHealth.isDead)
        {
            CancelInvoke("SpawnAbility"); // Stop the spawn loop

        }
        else if (!IsInvoking("SpawnAbility"))
        {
            // If the player is alive, ensure spawning continues
            InvokeRepeating("SpawnAbility", 0f, spawnRate);
        }
    }

    private void SpawnAbility()
    {
        // Get the screen width and height in world space
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect; // For orthographic cameras (2D)
        float screenHeight = mainCamera.orthographicSize * 2f; // For orthographic cameras (2D)

        // Randomize the X position within the screen width minus the padding
        float randomX = Random.Range(-screenWidth / 2 + horizontalPadding, screenWidth / 2 - horizontalPadding);

        // Spawn the ability just above the screen
        Vector3 spawnPosition = new Vector3(randomX, spawnHeightOffset, 0f); // Y position can be adjusted depending on your needs

        int randomIndex = Random.Range(0, abilityPrefab.Length);

        //Instantiate the ability prefab
        Instantiate(abilityPrefab[randomIndex], spawnPosition, Quaternion.identity);
    }
}
