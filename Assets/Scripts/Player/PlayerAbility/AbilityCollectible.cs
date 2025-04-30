using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectible : MonoBehaviour
{
    public AbilityManager.AbilityType abilityType;
    private AbilityManager abilityManager;

    void Start()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityManager abilityManager = other.GetComponent<AbilityManager>();
            if (abilityManager != null)
            {
                AudioManager.instance.PlayCollectAbilitySFX(); 
                abilityManager.CollectAbility(abilityType); // Add to the stack in Ability Manager script
                Destroy(gameObject);
            }
        }
    }
}
