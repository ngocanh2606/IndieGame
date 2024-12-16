using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShootPattern : IShootPattern
{
    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = angle + (i - (projectileCount - 1) / 2f) * spreadAngle; // Calculate the angle for each shot
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, currentAngle));
            // Add movement to the projectile (depending on your game mechanics)
        }
    }
}
