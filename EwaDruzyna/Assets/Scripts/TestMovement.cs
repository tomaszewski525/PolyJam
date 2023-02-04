using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 500.0f;
    public float downGravity = 20;
    private Rigidbody2D rigidBody2D;
    bool canJump = true;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(horizontalInput * speed, rigidBody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (rigidBody2D.velocity.y < -0.1)
        {
            rigidBody2D.gravityScale = downGravity;
        }
        else
        {
            rigidBody2D.gravityScale = 10;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
        }

    }


}