using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   
    public Joystick shootingJoystick; 

    //Timing
    public float shootInterval = 0.5f;  
    private float shootTimer;     
    private bool firstShot;      

    //Shooting
    public Transform firePoint;       
    [System.NonSerialized] public bool isShooting;  

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
                firstShot = true;
                shootTimer = 0;
            }

            // Shoot immediately on first touch, then use interval
            if (firstShot || shootTimer >= shootInterval)
            {
                AudioManager.instance.PlayShootSFX();
                Shoot();
                shootTimer = 0;
                firstShot = false;
            }

            
            shootTimer += Time.deltaTime;
        }
        else
        {
            AudioManager.instance.StopShootSFX();
            isShooting = false;
            firstShot = false;    // Reset first shot when joystick returns to center
        }
    }

    private void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            Vector2 shootDirection = new Vector2(shootingJoystick.Horizontal, shootingJoystick.Vertical).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }

    public bool IsShooting()
    {
        return isShooting;
    }
}
