using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Ability", menuName = "Bullet Ability")]
public class BulletAbility : ScriptableObject
{
    public string abilityName;
    public float damageAmount;
    public float speed;
    public float lifetime;
    public bool hasSpecialEffect;
    public string specialEffectType; // e.g., "poison", "explosion"

    // Add any other properties needed for unique abilities
}