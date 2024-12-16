using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootPattern
{
    void Shoot(Vector3 position, float angle, float spreadAngle, int projectileCount, GameObject projectilePrefab);
}
