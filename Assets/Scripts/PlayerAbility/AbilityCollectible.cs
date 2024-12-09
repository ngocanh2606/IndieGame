using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectible : MonoBehaviour
{
    //public Ability abilityPrefab; // Reference to the ability prefab
    public AbilityManager.AbilityType abilityType;  // Reference to the AbilityType you want to collect
    private AbilityManager abilityManager;

    void Start()
    {
        // Get the reference to the AbilityManager script
        abilityManager = FindObjectOfType<AbilityManager>();  // You could also assign this manually via the Inspector
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure only the player can collect
        {
            AbilityManager abilityManager = other.GetComponent<AbilityManager>();
            if (abilityManager != null)
            {
                //Ability abilityInstance = Instantiate(abilityPrefab); // Create a new instance
                abilityManager.CollectAbility(abilityType);
                Destroy(gameObject); // Destroy the collectible
            }
        }
    }
}
