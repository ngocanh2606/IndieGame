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


    void Start()
    {
        idleAngle = gunTransform.rotation.eulerAngles.z;
        firePointOffset = firePoint.localPosition;
    }
    void Update()
    {
        HandleShootingAnimation();
        RotateGunWithJoystick();
    }

    private void RotateGunWithJoystick()
    {
        // Get joystick input
        float x = playerShooting.shootingJoystick.Horizontal;
        float y = playerShooting.shootingJoystick.Vertical;

        // Only rotate if input is not zero
        if (x != 0 || y != 0)
        {
            Vector2 direction = new Vector2(x, y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(direction.x < 0)
            {
                sprite.flipX = true;
                gunTransform.rotation = Quaternion.Euler(0f, 0f, angle-180);

            }
            else if (direction.x > 0)
            {
                sprite.flipX = false;
                gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            }
        }
        else
        {
            sprite.flipX = playerMovement.sprite.flipX;
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
