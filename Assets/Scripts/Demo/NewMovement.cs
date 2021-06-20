using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    // horizontal constants
    public float HStartAccel, HStopAccel, HMaxVel;
    // vertical constants
    public float JumpStrength;
    // different gravity constants for parts of jump
    public float ClimbGravity, HangGravity, FallGravity;
    // constants to check for transitions in jump arcs
    public float HangVelocity;
    // velocity state variables
    public float XVel, YVel;
    // input containers
    public float hIn, vIn;

    public bool OnGround;
    public bool FacingRight = true;

    private void Update()
    {
        hIn = Input.GetAxisRaw("Horizontal");
        vIn = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            ApplyJumpForce();

        // sprite dir
        if (FacingRight && rb.velocity.x < 0f || !FacingRight && rb.velocity.x > 0f)
        {
            FacingRight = !FacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
    }

    private void FixedUpdate()
    {
        // horizontal movement
        if (hIn < 0f)
        {
            XVel = -HMaxVel;
        }
        else if (hIn > 0f)
        {
            XVel = HMaxVel;
        }
        else
        {
            XVel = 0f;
        }

        // apply gravity when not grounded
        if (!OnGround)
        {
            // climb
            if (rb.velocity.y > 0f && rb.velocity.y > HangVelocity)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (ClimbGravity - 1) * Time.fixedDeltaTime;
            }
            // fall
            else if (rb.velocity.y < 0f && rb.velocity.y < -HangVelocity)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (FallGravity - 1) * Time.fixedDeltaTime;
            }
            // hang
            else 
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (HangGravity - 1) * Time.fixedDeltaTime;
            }
        }

        // apply jump impulse if jump is input


        // apply movement to player
        //rb.MovePosition((Vector2) transform.position + new Vector2(XVel * Time.fixedDeltaTime, YVel * Time.fixedDeltaTime));
        rb.velocity = new Vector2(XVel, rb.velocity.y);
    }

    private void ApplyJumpForce()
    {
        if (OnGround)
        {
            rb.velocity += Vector2.up * JumpStrength;
            OnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnGround = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!OnGround)
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // wait few seconds for coyote time
        OnGround = false;
    }

    private bool CheckBelow()
    {
        return false;
    }

}
