using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 100f;
    [SerializeField] private float dashDuration = 1.077f;  
    private Rigidbody2D rb;                             
    [System.NonSerialized] public bool isDashing = false;  

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector2 dashDirection)
    {
        StartCoroutine(Dash(dashDirection));
    }

    private IEnumerator Dash(Vector2 dashDirection)
    {
        isDashing = true;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero; 
        isDashing = false;
    }
}
