using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private Rigidbody2D rb;
    [System.NonSerialized] public bool isDashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Assumes the script is attached to the same GameObject as the Rigidbody
    }

    public void Activate()
    {
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward; // Modify based on desired input direction
        rb.velocity = dashDirection * dashSpeed;

        Debug.Log("Gravity Dash Ability Activated!");

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Reset velocity after dash
        isDashing = false;

        Debug.Log("Gravity Dash Ability Deactivated!");

    }
}
