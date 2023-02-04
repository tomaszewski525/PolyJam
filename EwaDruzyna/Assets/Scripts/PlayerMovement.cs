using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D collision;
    public float maxVelocity = 5.0f;
    public float maxJumpSpeed = 50.0f;
    public float moveForce = 2;
    public float jumpForce = 50;
    private float frameTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
        jumpForce = 30;
        moveForce = 2;
        maxJumpSpeed = 30;
        maxVelocity = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        frameTime = Time.deltaTime;
        Move(GetInput());
    }


    Vector2 GetInput()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.Space))
        {
            dir.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += +1;
        }
        return dir;
    }
    private bool isGrounded = true;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void Move(Vector2 movedirection)
    {
        
        // Move player on ground
       if(Mathf.Abs(rb.velocity.x) < maxVelocity)
       {
            Vector2 flatMove = new Vector2(movedirection.x, 0.0f);
            Vector3 flatMoveVec3 = Vector3.ClampMagnitude(flatMove.normalized* moveForce, maxVelocity);
            rb.AddForce(flatMoveVec3);
       }





    }
}
