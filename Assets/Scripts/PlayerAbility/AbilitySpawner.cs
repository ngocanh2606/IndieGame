using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject[] abilityPrefab; // Reference to the ability prefab
    public float spawnRate = 2f; // Rate at which abilities spawn (in seconds)

    public float spawnHeightOffset = 10f; // The height from which it will fall (in world space)
    public float horizontalPadding = 0.5f; // Horizontal padding to prevent spawning too close to screen edges
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;

        // Start the spawn loop
        InvokeRepeating("SpawnAbility", 0f, spawnRate);
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
