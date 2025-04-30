using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShootPattern : IShootPattern
{
    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = angle + (i * 360f / projectileCount);
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, currentAngle));
        }
    }
}
