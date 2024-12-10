using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private Rigidbody rb;
    private bool isDashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Assumes the script is attached to the same GameObject as the Rigidbody
    }

    public void Activate()
    {
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward; // Modify based on desired input direction
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero; // Reset velocity after dash
        isDashing = false;
    }
}
