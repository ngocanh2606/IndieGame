using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectible : MonoBehaviour
{
    public Ability abilityPrefab; // Reference to the ability prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player can collect
        {
            AbilityManager abilityManager = other.GetComponent<AbilityManager>();
            if (abilityManager != null)
            {
                Ability abilityInstance = Instantiate(abilityPrefab); // Create a new instance
                abilityManager.CollectAbility(abilityInstance);
                Destroy(gameObject); // Destroy the collectible
            }
        }
    }
}
