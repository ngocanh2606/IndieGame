using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public PlayerCharacterState currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetState(PlayerCharacterState.Idle);
    }

    public void SetState(PlayerCharacterState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        animator.SetInteger("AnimState", (int)newState);
    }
}
