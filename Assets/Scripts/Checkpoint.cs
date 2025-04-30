using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [System.NonSerialized] public bool reached = false;
    [SerializeField] private GameObject go;

    private void Start()
    {
        go.SetActive(false);
    }

    private void Update()
    {
        if (TutorialManager.instance.currentStep == TutorialStep.ReachCheckpoint)
        {
            go.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            reached = true;
        }
    }
}
