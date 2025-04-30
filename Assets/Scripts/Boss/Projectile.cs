using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;
    public float damage = 1;

    public Animator anim;
    private bool hasExploded = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (!hasExploded)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().OnTakeDamage(damage);
            StartCoroutine(WaitForExplodeAnimation());
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(WaitForExplodeAnimation());
        }
    }

    private IEnumerator WaitForExplodeAnimation()
    {
        hasExploded = true;
        speed = 0f;
        anim.SetTrigger("Explode");

        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}
