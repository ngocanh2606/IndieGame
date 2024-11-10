using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public BulletAbility bulletAbility; // Assign the ScriptableObject in the Inspector

    public float damageAmount;

    void Start()
    {
        damageAmount = bulletAbility.damageAmount;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("IS COLLIDING");
    //    if (other.CompareTag("Player"))
    //    {
    //        // Assuming the player has a health script with a TakeDamage method
    //        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(bulletAbility.damageAmount);
    //        }

    //        Destroy(gameObject); // Destroy bullet on impact
    //    }
    //}
}
