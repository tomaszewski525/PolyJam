using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUsingGravity : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 500.0f;
    public float downGravity = 2000;

    private Rigidbody2D rigidBody2D;
    bool isGrounded = true;
    bool canJump = true;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {


        // Horizontal move
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(horizontalInput * speed, rigidBody2D.velocity.y);

        // Can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
        }

        // Change Gravity when falling
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
            isGrounded = true;
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);

        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}

