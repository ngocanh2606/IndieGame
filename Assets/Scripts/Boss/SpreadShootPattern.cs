using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShootPattern : IShootPattern
{
    private int offset = 10;

    public void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab)
    {
        // Make the second shot go in the opposite direction, then randomize the offset again
        if (offset < 0)
        {
            offset = Random.Range(1, 10);
        }
        else
        {
            offset = -offset; // Reverse the offset direction
        }

        for (int i = 0; i < projectileCount; i++)
        {
            float currentAngle = angle + (i - (projectileCount - 1) / 2f) * spreadAngle + offset;
            GameObject projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, currentAngle));
        }
    }
}
