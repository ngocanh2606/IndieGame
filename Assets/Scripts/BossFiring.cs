using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFiring : MonoBehaviour
{
    public Transform firePoint;          // Point from where bullets are fired
    public GameObject[] bulletPrefabs;   // Array of different bullet prefabs

    public void FireBullet(int bulletIndex)
    {
        if (bulletIndex < bulletPrefabs.Length)
        {
            Instantiate(bulletPrefabs[bulletIndex], firePoint.position, firePoint.rotation);
        }
    }

    // Example usage: call FireBullet(0) to fire the first bullet type, FireBullet(1) for the second, etc.
}
