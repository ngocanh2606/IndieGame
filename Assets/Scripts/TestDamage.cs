using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    private BulletAbility bulletAbility; // Assign the ScriptableObject in the Inspector

    private void Start()
    {
        InitBulletAbility("ability_1");
    }

    public void InitBulletAbility(string abilityName)
    {
        bulletAbility = BulletAbilityManagerSO.Instance.GetBulletAbility(abilityName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IS COLLIDING");
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
        if (other.TryGetComponent<IDamageable>(out var hit))
        {
            Debug.Log(bulletAbility);
            hit.OnTakeDamage(bulletAbility.damageAmount);

            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}