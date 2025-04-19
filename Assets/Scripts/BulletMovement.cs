using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    public int damage = 1;
    private Rigidbody2D rb;

    public Animator anim;

    private bool hasExploded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // Destroy after lifetime if it doesn't hit anything
    }

    void Update()
    {
        if (!hasExploded)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasExploded) return;

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BossController>()?.TakeDamage(damage);
            StartCoroutine(WaitForExplodeAnimation());
        }
        else if (collision.CompareTag("Ground"))
        {
            StartCoroutine(WaitForExplodeAnimation());
        }
    }

    private IEnumerator WaitForExplodeAnimation()
    {
        hasExploded = true;
        speed = 0f;
        anim.SetTrigger("Explode");

        // Optional: Disable collider to prevent double triggering
        GetComponent<Collider2D>().enabled = false;

        // Wait for animation to finish before destroying
        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}
