using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    public int damage = 1;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Destroy after the lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle damage or destruction on collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply damage or any other effect
            collision.gameObject.GetComponent<BossController>().TakeDamage(damage);
            Destroy(gameObject); // Destroy projectile on hit
        }
    }
}