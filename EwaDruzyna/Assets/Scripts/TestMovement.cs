using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    // Animal Data
    bool canJump = true;
    bool canMove = true;
    public float speed = 10.0f;
    public float jumpForce = 500.0f;
    public float gravityDownGravity = 3000;
    public float gravityNormalForce = 1000.0f;
    animalType animal = animalType.Spider;

    // Temp variables
    float gravityForce = 1000.0f;
    int gravityDir = 0;
    Vector2 gravity = Vector2.zero;
    bool isGrounded = true;
    private Rigidbody2D rigidBody2D;
    public RotatingAimIndicator indicator;
    public Camera camera;
    public enum animalType
    {
        Spider, Ladybird, Crawler
    }
    public enum specialAbilityType
    {
        ShootLine, Gliding
    }

    // hold space variables
    bool isHoldingSpace = false;
    bool wasHoldingSpace = false;
    float spaceHoldTime = 0.003f;
    float totalSpaceHoldTime = 0.0f;
    

    

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        //Transform cameraTransform = transform.Find("Main Camera");
        //camera = cameraTransform.GetComponent<Camera>();
        rigidBody2D.gravityScale = 0;

        //GameObject.Find("aimIndicator").GetComponent<RotatingAimIndicator>();
        indicator = GameObject.Find("AimIndicatorBox").GetComponentInChildren<RotatingAimIndicator>();
    }

    private void Update()
    {
        //print(indicator.enemyInBounds.name);
        if(indicator.enemyInBounds != null)
        {
           print(indicator.enemyInBounds);
        }
        //print(indicator.enemyInBounds);

        ChooseGravityDirection(gravityDir);
        ChangeGravityForce();

        CheckIfSpaceIsHeld();

        if (canMove)
        {
            Move();
        }

        if(canJump)
        {
            Jump();
        }
    }

    public void CheckIfSpaceIsHeld()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            if (wasHoldingSpace)
            {
                totalSpaceHoldTime += Time.deltaTime;
            }
            else
            {
                wasHoldingSpace = true;
                totalSpaceHoldTime += Time.deltaTime;
            }
        }
        else
        {
            wasHoldingSpace = false;
            totalSpaceHoldTime = 0;
        }

        if (totalSpaceHoldTime > spaceHoldTime)
        {
            isHoldingSpace = true;
        }
        else
        {
            isHoldingSpace = false;
        }
    }
    public void Move()
    {
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

    }

    public void Jump()
    {
        // Can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            print(jumpForce * gravity);
            rigidBody2D.AddForce(jumpForce * gravity * (-1));
        }

    }

    public void ChangeGravityForce()
    {
        float tempDownGravity = 0;

        if(isHoldingSpace && animal == animalType.Ladybird)
        {
            tempDownGravity = gravityNormalForce / 4;
        }
        else
        {
            tempDownGravity = gravityDownGravity;
        }

        if (gravityDir == 0)
        {
            if(rigidBody2D.velocity.y < -0.1)
            {
                gravityForce = tempDownGravity;
            }
            else
            {
                gravityForce = gravityNormalForce;
            }
        }
        if (gravityDir == 1)
        {
            if (rigidBody2D.velocity.y > 0.1)
            {
                gravityForce = tempDownGravity;
            }
            else
            {
                gravityForce = gravityNormalForce;
            }
        }

        if (gravityDir == 3)
        {
            if (rigidBody2D.velocity.x < -0.1)
            {
                gravityForce = tempDownGravity;
            }
            else
            {
                gravityForce = gravityNormalForce;
            }
        }

        if (gravityDir == 2)
        {
            if (rigidBody2D.velocity.x > 0.1)
            {
                gravityForce = tempDownGravity;
            }
            else
            {
                gravityForce = gravityNormalForce;
            }
        }



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
    public void ChooseGravityDirection(int direction)
    {
        if (direction == 0)
        {
            gravity = new Vector2(0, -1);
            //transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w));
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            //camera.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }
        if (direction == 1)
        {
            gravity = new Vector2(0, 1);
            //transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 90, transform.rotation.w));
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
            //camera.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }
        if (direction == 2)
        {
            gravity = new Vector2(1, 0);
            //transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w));
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
            //camera.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }
        if (direction == 3)
        {
            gravity = new Vector2(-1, 0);
            //transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, 270, transform.rotation.w));
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 270);
           // camera.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
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

