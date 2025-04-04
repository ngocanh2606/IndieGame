using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShootPattern : IShootPattern
{
    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = angle + (i * 360f / projectileCount); // Calculate the angle for each shot
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, currentAngle));
            // Add movement to the projectile
            //0, does not matter/0, 10
        }
    }
}
