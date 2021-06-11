using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Constants
    private const float Acceleration = 0.2f, MaxSpeed = 5f;

    // State information
    private bool OnGround;
    private float HorizontalMovement;

    // Components
    public Rigidbody2D rb;

    private void Update()
    {
        // check input
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // apply horizontal acceleration
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + HorizontalMovement * Acceleration, -MaxSpeed, MaxSpeed), rb.velocity.y);

        // stop player if no input
        if (HorizontalMovement == 0) {
            rb.velocity = new Vector2(rb.velocity.x / 1.25f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = true;
    }
}
