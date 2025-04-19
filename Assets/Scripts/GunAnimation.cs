using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    public PlayerShooting playerShooting;
    public Animator gunAnimator;
    public Transform gunTransform;

    public SpriteRenderer sprite;

    private float idleAngle;
    private bool flip;
    public PlayerMovement playerMovement;

    public Transform firePoint;
    public Vector3 firePointOffset; // adjust as needed

    public Transform playerTransform;
    private Transform playerLastTransform;

    void Start()
    {
        idleAngle = gunTransform.rotation.eulerAngles.z;
        firePointOffset = firePoint.localPosition;
        playerLastTransform = playerTransform;
    }
    void Update()
    {
        if (playerTransform.rotation != playerLastTransform.rotation)
        {
            gunTransform.rotation = playerTransform.rotation;
            playerLastTransform.rotation = playerTransform.rotation;
        }

        idleAngle = playerTransform.eulerAngles.z;

        HandleShootingAnimation();
        RotateGunWithJoystick();


    }

    private void RotateGunWithJoystick()
    {
        float x = 0;
        float y = 0;

        // Get joystick input
        x = playerShooting.shootingJoystick.Horizontal;
        y = playerShooting.shootingJoystick.Vertical;

        // Only rotate if input is not zero
        if (x != 0 || y != 0)
        {
            Vector2 direction = new Vector2(x, y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //Gravity is down
            if (idleAngle == 0f)
            {
                Debug.Log("Gravity is down");
                if (direction.x < 0)
                {
                    sprite.flipX = true;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle - 180);

                }
                else if (direction.x > 0)
                {
                    sprite.flipX = false;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);

                }
            }

            //Gravity is left
            else if (idleAngle == 270f)
            {
                Debug.Log("Gravity is left");
                if (direction.y > 0)
                {
                    sprite.flipX = true;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
                }
                else if (direction.y < 0)
                {
                    sprite.flipX = false;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }

            //Gravity is right
            else if (idleAngle == 90f)
            {
                Debug.Log("Gravity is right");
                if (direction.y < 0)
                {
                    sprite.flipX = true;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
                }
                else if (direction.y > 0)
                {
                    sprite.flipX = false;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }

            //Gravity is up
            else if (idleAngle == 180f)
            {
                Debug.Log("Gravity is up");
                if (direction.x > 0)
                {
                    sprite.flipX = true;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle - 180);
                }
                else if (direction.x < 0)
                {
                    sprite.flipX = false;
                    gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }
        }
        else
        {
            sprite.flipX = playerMovement.sprite.flipX;
            gunTransform.rotation = playerTransform.rotation;
        }

        firePoint.localPosition = sprite.flipX ? new Vector3(-firePointOffset.x, firePointOffset.y, firePointOffset.z) : firePointOffset;

    }

    private void HandleShootingAnimation()
    {
        if (playerShooting.IsShooting())
        {
            gunAnimator.SetBool("IsShooting", true);
        }
        else
        {
            gunAnimator.SetBool("IsShooting", false);
            gunTransform.rotation = Quaternion.Euler(0f, 0f, idleAngle);
        }
    }
}
