using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Constants
    private const float Acceleration = 0.2f, MaxSpeed = 5f;
    private const float JumpSpeed = 7f;

    // State information
    public bool OnGround;
    private float HorizontalMovement;
    public bool JumpInput;

    // Components
    public Rigidbody2D rb;

    private void Update()
    {
        // check input
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        float xVelocity = rb.velocity.x, yVelocity = rb.velocity.y;

        // apply horizontal acceleration
        xVelocity = Mathf.Clamp(xVelocity + HorizontalMovement * Acceleration, -MaxSpeed, MaxSpeed);

        // stop player if no input
        if (HorizontalMovement == 0) {
            xVelocity = xVelocity / 1.25f;
        }

        // jump only if on the ground
        if (JumpInput && OnGround)
        {
            yVelocity = JumpSpeed;
        }

        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colliding");
        OnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Jumping");
        OnGround = false;
    }
}
