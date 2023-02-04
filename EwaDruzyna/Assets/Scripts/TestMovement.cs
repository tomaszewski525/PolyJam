using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 500.0f;


    private Rigidbody2D rigidBody2D;
    bool isGrounded = true;
    bool canJump = true;

    public float downGravity = 20;
    public float gravityForce = 10.0f;
    int gravityDir = 0;
    
    public enum animalType
    {
        Spider, Ladybird, Crawler
    }

    public enum specialAbilityType
    {
        ShootLine
    }

    animalType animal = animalType.Ladybird;
    

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.gravityScale = 0;
    }

    private void Update()
    {

        ChooseGravityDirection(gravityDir);
        // Horizontal move
        if (gravity.x == 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rigidBody2D.velocity = new Vector2(horizontalInput * speed, rigidBody2D.velocity.y);
        }
        else
        {
            float verticalInput = Input.GetAxis("Vertical");
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, verticalInput * speed);
        }

        // Can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            print(jumpForce * gravity);
            rigidBody2D.AddForce(jumpForce* gravity*(-1));
        }
        
        /*
        // Change Gravity when falling
        if (rigidBody2D.velocity.y < -0.1)ad
        {
            rigidBody2D.gravityScale = downGravity;
        }
        else
        {
            rigidBody2D.gravityScale = 10;
        }*/
    }

    public void collisionEnterHandler(Vector2 stopVelocity, int gravityDirection)
    {
        if (gravityDir == gravityDirection)
        {
            isGrounded = true;
            rigidBody2D.velocity = stopVelocity;
        }

        if (animal == animalType.Spider)
        {
            gravityDir = gravityDirection;
            isGrounded = true;
            rigidBody2D.velocity = stopVelocity;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            collisionEnterHandler(new Vector2(rigidBody2D.velocity.x, 0), 0);
        }
        if (collision.collider.tag == "Ceiling")
        {
            collisionEnterHandler(new Vector2(rigidBody2D.velocity.x, 0), 1);
        }
        if (collision.collider.tag == "RightSide")
        {
            collisionEnterHandler(new Vector2(0, rigidBody2D.velocity.y), 2);
        }
        if (collision.collider.tag == "LeftSide")
        {
            collisionEnterHandler(new Vector2(0, rigidBody2D.velocity.y), 3);
        }

    }

    Vector2 gravity = Vector2.zero;
    public void ChooseGravityDirection(int direction)
    {
        if (direction == 0)
        {
            gravity = new Vector2(0, -1);
        }
        if (direction == 1)
        {
            gravity = new Vector2(0, 1);
        }
        if (direction == 2)
        {
            gravity = new Vector2(1, 0);
        }
        if (direction == 3)
        {
            gravity = new Vector2(-1, 0);
        }

        // Apply gravity
        rigidBody2D.AddForce(Time.deltaTime* gravity*gravityForce);
    }


    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor" && gravityDir == 0)
        {
            isGrounded = false;
        }
        if (col.gameObject.tag == "Ceiling" && gravityDir == 1)
        {
            isGrounded = false;
        }
        if (col.gameObject.tag == "RightSide" && gravityDir == 2)
        {
            isGrounded = false;
        }
        if (col.gameObject.tag == "LeftSide" && gravityDir == 3)
        {
            isGrounded = false;
        }
    }
}

