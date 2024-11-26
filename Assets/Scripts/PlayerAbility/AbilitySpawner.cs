using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject abilityPrefab; // Reference to the ability prefab
    public float spawnRate = 2f; // Rate at which abilities spawn (in seconds)
    public float spawnHeight = 10f; // Height at which the abilities spawn
    public float fallSpeed = 3f; // Speed at which the abilities fall

    private void Start()
    {
        // Start the spawn loop
        InvokeRepeating("SpawnAbility", 0f, spawnRate);
    }

    private void SpawnAbility()
    {
        // Spawn ability at a random x position at the top of the screen
        float randomX = Random.Range(-8f, 8f); // Adjust the range based on screen size
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);

        // Instantiate the ability prefab
        GameObject ability = Instantiate(abilityPrefab, spawnPosition, Quaternion.identity);

        // Set the fall speed of the ability
        ability.GetComponent<FallingAbility>().fallSpeed = fallSpeed;
    }
}
