using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShootPattern : IShootPattern
{
    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, angle));
        }
    }
}
