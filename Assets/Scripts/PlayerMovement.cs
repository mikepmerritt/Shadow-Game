using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Constants
    private const float Acceleration = 0.2f, MaxSpeed = 5f;
    private const float JumpSpeed = 15f;

    // State information
    private bool OnGround;

    // Components
    public Rigidbody2D rb;

    public void Move(float horizontalMovement, bool jumpInput)
    {
        float xVelocity = rb.velocity.x, yVelocity = rb.velocity.y;

        // ground movement
        if (OnGround) 
        {
            // apply horizontal acceleration
            xVelocity = Mathf.Clamp(xVelocity + horizontalMovement * Acceleration, -MaxSpeed, MaxSpeed);

            // stop player if no input
            if (horizontalMovement == 0 || (Mathf.Abs(xVelocity + horizontalMovement) - Mathf.Abs(xVelocity) + Mathf.Abs(horizontalMovement)) < 0.00001) {
                //Debug.Log("stopping, xvel = " + xVelocity + ", horiz.in = " + HorizontalMovement);
                xVelocity = 0;
            }
        }

        // jump only if on the ground
        if (jumpInput && OnGround)
        {
            //yVelocity = JumpSpeed;
            yVelocity = Mathf.Sqrt(Mathf.Pow(JumpSpeed, 2) - Mathf.Pow(xVelocity, 2));
        }

        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void HitGround()
    {
        OnGround = true;
    }

    public void LeaveGround()
    {
        OnGround = false;
    }
}
