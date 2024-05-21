using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private InputController inputController;
    private float xMove;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        rb2d = GetComponent<Rigidbody2D>();
        inputController.onMoveEvent += GetMove;
    }

    private void GetMove(float value)
    {
        xMove = value;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // speed는 나중에 다시 구현할 것.
        rb2d.velocity = new Vector2(xMove, 0) * Time.deltaTime * 500;
    }
}
