using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAbility : MonoBehaviour
{
    [SerializeField] private float gravityFreeDuration = 5f;    // Duration the gravity-free effect lasts
    [System.NonSerialized] public bool isGravityFree = false;   // To track if the ability is currently active

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        StartCoroutine(GravityFreeEffect());
    }

    private IEnumerator GravityFreeEffect()
    {
        isGravityFree = true;
        rb.gravityScale = 1; // Weaker gravity => Jump higher

        // Wait for the duration of the ability
        yield return new WaitForSeconds(gravityFreeDuration);

        // Re-enable initial gravity value after the ability duration
        rb.gravityScale = 3;
        isGravityFree = false;
    }
}
