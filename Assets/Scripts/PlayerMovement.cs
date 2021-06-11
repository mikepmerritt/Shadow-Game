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
    private float HorizontalMovement;
    private bool JumpInput;

    // Components
    public Rigidbody2D rb;

    private void Update()
    {
        // check input
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        float xVelocity = rb.velocity.x, yVelocity = rb.velocity.y;

        // ground movement
        if (OnGround) 
        {
            // apply horizontal acceleration
            xVelocity = Mathf.Clamp(xVelocity + HorizontalMovement * Acceleration, -MaxSpeed, MaxSpeed);

            // stop player if no input
            if (HorizontalMovement == 0) {
                xVelocity = xVelocity / 1.25f;
            }
        }

        // jump only if on the ground
        if (JumpInput && OnGround)
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
