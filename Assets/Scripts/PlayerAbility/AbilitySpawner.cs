using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject[] abilityPrefab; // Reference to the ability prefab
    public Transform spawnPoint;
    public float spawnRate = 2f; // Rate at which abilities spawn (in seconds)
    public float spawnHeight = 10f; // Height at which the abilities spawn
    public float spawnRange = 8f;
    //public float fallSpeed = 3f; // Speed at which the abilities fall

    private void Start()
    {
        // Start the spawn loop
        InvokeRepeating("SpawnAbility", 0f, spawnRate);
    }

    private void SpawnAbility()
    {
        //// Spawn ability at a random x position at the top of the screen
        //float randomX = Random.Range(-spawnRange, spawnRange); // Adjust the range based on screen size
        //Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);

        //int randomIndex = Random.Range(0, abilityPrefab.Length); // Random index between 0 and 2 (since we have 3 abilities)

        //Transform spawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
        // Instantiate the ability prefab
        //Instantiate(abilityPrefab[randomIndex], spawnPosition, Quaternion.identity);

        //// Set the fall speed of the ability
        //ability.GetComponent<FallingAbility>().fallSpeed = fallSpeed;
    }
}
