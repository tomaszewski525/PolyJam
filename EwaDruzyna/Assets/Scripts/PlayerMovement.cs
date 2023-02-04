using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxVelocity = 5.0f;
    public float maxJumpSpeed = 50.0f;
    public float moveForce = 2;
    public float jumpForce = 50;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = 30;
        moveForce = 2;
        maxJumpSpeed = 30;
        maxVelocity = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
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

    void Move(Vector2 movedirection)
    {
        // Move player on ground
        if(Mathf.Abs(rb.velocity.x) < maxVelocity)
        {
            Vector2 flatMove = new Vector2(movedirection.x, 0.0f);
            Vector3 flatMoveVec3 = Vector3.ClampMagnitude(flatMove.normalized* moveForce, maxVelocity);

            
            Vector2 jumpMove = new Vector2(0.0f, movedirection.y);
            Vector3 jumpMoveVec3 = Vector3.ClampMagnitude(jumpMove.normalized* jumpForce, maxJumpSpeed);


            // print(jumpMoveVec3 + flatMoveVec3);
            print(jumpMoveVec3+flatMoveVec3); 
            rb.AddForce(jumpMoveVec3 + flatMoveVec3);

   
        }





    }
}
