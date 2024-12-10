using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
    public AbilityManager.AbilityType type; // Ability type
    public Sprite sprite; // Icon for the ability

    public virtual void Activate()
    {
        Debug.Log("Ability activated: " + type);
    }
}