using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collider2D collision;
    public float buttonTime = 0.3f;
    public float jumpAmount = 20;
    bool jumping;
    bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;

        }
        if (jumping && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
        }
        

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }
    }

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
}
