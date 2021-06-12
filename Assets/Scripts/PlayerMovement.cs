using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Constants
    private const float Acceleration = 0.2f, MaxSpeed = 5f;
    private const float JumpSpeed = 15f;
    private const float PlayerWidth = 0.625f / 2f;

    // State information
    private bool OnGround;

    // Components and references
    public Rigidbody2D rb;
    public Collider2D playerCollider;
    public GameObject floorCheck;
    public LayerMask GroundLayer;

    public void Move(float horizontalMovement, bool jumpInput)
    {
        float xVelocity = rb.velocity.x, yVelocity = rb.velocity.y;

        Collider2D[] detected = Physics2D.OverlapCircleAll(floorCheck.transform.position, PlayerWidth, GroundLayer);

        Debug.Log(detected.Length);

        bool foundGround = false;
        foreach (Collider2D collider in detected)
        {
            if (collider != playerCollider)
            {
                foundGround = true;
            }
        }
        OnGround = foundGround;

        Debug.Log(gameObject.name + OnGround);

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
        else
        {
            // apply lesser horizontal acceleration
            xVelocity = Mathf.Clamp(xVelocity + horizontalMovement * (Acceleration / 4f), -MaxSpeed, MaxSpeed);

            // slow player if no input
            if (horizontalMovement == 0 || (Mathf.Abs(xVelocity + horizontalMovement) - Mathf.Abs(xVelocity) + Mathf.Abs(horizontalMovement)) < 0.00001) {
                //Debug.Log("stopping, xvel = " + xVelocity + ", horiz.in = " + HorizontalMovement);
                xVelocity = xVelocity / 1.25f;
            }
        }

        // jump only if on the ground
        if (jumpInput && OnGround)
        {
            //yVelocity = JumpSpeed;
            yVelocity = Mathf.Sqrt(Mathf.Pow(JumpSpeed, 2) - Mathf.Pow(xVelocity, 2));
            OnGround = false;
        }

        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
