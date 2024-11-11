using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public BulletAbility bulletAbility; // Assign the ScriptableObject in the Inspector

    private float lifetimeTimer;

    void Start()
    {
        lifetimeTimer = bulletAbility.lifetime;
    }

    void Update()
    {
        // Move the bullet forward at its specified speed
        transform.Translate(Vector2.up * bulletAbility.speed * Time.deltaTime);

        // Check lifetime and destroy if exceeded
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Assuming the player has a health script with a TakeDamage method
        //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        //if (playerHealth != null)
        //{
        //    playerHealth.TakeDamage(bulletAbility.damageAmount);
        //}

        if (other.TryGetComponent<IDamageable>(out var hit))
        {
            hit.OnTakeDamage(bulletAbility.damageAmount);

            // Handle any special effects
            if (bulletAbility.hasSpecialEffect)
            {
                ApplySpecialEffect(other);
            }

            Destroy(gameObject); // Destroy bullet on impact
        }

    }

    private void ApplySpecialEffect(Collider2D player)
    {
        // Apply the effect based on the type
        switch (bulletAbility.specialEffectType)
        {
            case "poison":
                // Implement poison effect
                Debug.Log("Poison effect applied to player");
                break;

            case "explosion":
                // Implement explosion effect
                Debug.Log("Explosion effect applied to player");
                break;

            case "slow":
                // Implement slow effect
                Debug.Log("Slow effect applied to player");
                break;

            default:
                Debug.Log("No special effect applied.");
                break;
        }
    }
}
