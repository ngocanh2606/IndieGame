using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed for sliding
    private Vector2 moveDirection = Vector2.zero;

    void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            MovePlayer();
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    private void MovePlayer()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void MoveSingleUnit(Vector2 direction, float singleUnit)
    {
        transform.Translate(direction * singleUnit);
    }
}
