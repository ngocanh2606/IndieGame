using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;
    public float damage = 1;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Move the projectile forward in the direction it's facing
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    // Handle collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle damage or destruction on collision
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().OnTakeDamage(damage);
            Destroy(gameObject); 
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); 
        }
    }
}
