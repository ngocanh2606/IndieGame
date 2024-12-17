using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShootPattern : IShootPattern
{
    private float angleOffset = 0f;

    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = angle + angleOffset + (i * 10f); // Increase the angle slightly for each shot to form a spiral
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, currentAngle));
            // Add movement to the projectile
        }

        angleOffset += 5f;  // Increase the spiral's angle for the next shot round
    }
}
