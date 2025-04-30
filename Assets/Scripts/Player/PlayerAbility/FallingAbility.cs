using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAbility : MonoBehaviour
{
    public float fallSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
