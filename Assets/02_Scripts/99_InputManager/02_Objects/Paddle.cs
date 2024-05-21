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
        rb2d.velocity = Vector2.right * xMove * Time.deltaTime * 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var nall = collision.gameObject.GetComponent<Icollidable>();
        if (nall != null)
        {
            nall.OnCollide(this.gameObject);
        }
    }

}
