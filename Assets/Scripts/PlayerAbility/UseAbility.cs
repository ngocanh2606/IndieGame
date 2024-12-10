using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseAbility : MonoBehaviour
{
    public string abilityName;  // Name of the ability for identification
    public float cooldownTime; // Cooldown time for the ability

    public abstract void Activate(); // Method to activate the ability
}
