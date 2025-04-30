using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [System.NonSerialized] public bool reached = false;
    public GameObject go;

    private void Start()
    {
        go.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            reached = true;
            go.SetActive(false);
        }
    }
}
