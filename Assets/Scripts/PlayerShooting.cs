using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Joystick shootingJoystick;      // Reference to the joystick for shooting
    public GameObject bulletPrefab;        // Bullet prefab for shooting
    public Transform firePoint;            // Point from where bullets are fired
    public float shootInterval = 0.5f;     // Time interval between subsequent shots
    //public float bulletSpeed = 10f;        // Speed of the bullet

    private bool isShooting;               // Track if player is shooting
    private float shootTimer;              // Timer to manage shoot intervals
    private bool firstShot;                // Track the first shot on touch

    void Update()
    {
        HandleShootingInput();
    }

    private void HandleShootingInput()
    {
        // Check if the joystick is being used
        if (shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0)
        {
            if (!isShooting)
            {
                isShooting = true;
                firstShot = true; // Set first shot flag
                shootTimer = 0;   // Reset timer
            }

            // Shoot immediately on first touch, then use interval
            if (firstShot || shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0;
                firstShot = false; // First shot done, rely on interval now
            }

            // Increment timer for subsequent shots
            shootTimer += Time.deltaTime;
        }
        else
        {
            isShooting = false;
            firstShot = false;    // Reset first shot when joystick returns to center
        }
    }

    private void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            // Calculate the direction from joystick input
            Vector2 shootDirection = new Vector2(shootingJoystick.Horizontal, shootingJoystick.Vertical).normalized;

            // Instantiate a bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Set the bullet's rotation to face the direction
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust angle to point correctly

            // Apply velocity to the bullet in the shoot direction
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    rb.velocity = shootDirection * bulletSpeed;
            //}

            Debug.Log("Shooting bullet in direction: " + shootDirection);
        }
    }
}
