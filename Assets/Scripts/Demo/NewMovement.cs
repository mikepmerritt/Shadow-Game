using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Collider2D coll;

    // horizontal constants
    private float HStartAccel, HStopAccel, HMaxVel = 5f;
    // vertical constants
    private float JumpStrength = 11f;
    // different gravity constants for parts of jump
    private float ClimbGravity = 2.5f, HangGravity = 6f, FallGravity = 4.25f;
    private float LowJumpFactor = 4f;
    // constants to check for transitions in jump arcs
    private float HangVelocity = 5f;
    // velocity state variables
    public float XVel, YVel;
    // input containers
    private float hIn, vIn;

    public bool OnGround;
    public bool FacingRight = true;

    public bool CoyoteTimeAvailable, CoyoteTimeActive;
    public float CoyoteTimer;
    private float CoyoteTimeDuration = 0.08f;

    private void Update()
    {
        hIn = Input.GetAxisRaw("Horizontal");
        vIn = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            ApplyJumpForce();

        // sprite dir
        if (FacingRight && rb.velocity.x < -0.0001f && hIn < 0f || !FacingRight && rb.velocity.x > 0.0001f && hIn > 0f)
        {
            FacingRight = !FacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (CoyoteTimer <= 0f && CoyoteTimeActive)
        {
            CoyoteTimeActive = false;
            CoyoteTimer = 0f;
        }
        else if (CoyoteTimeActive)
        {
            CoyoteTimer -= Time.deltaTime;
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

        OnGround = IsGrounded();

        // apply gravity when not grounded
        if (!OnGround)
        {
            // climb
            if (rb.velocity.y > 0f && rb.velocity.y > HangVelocity)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (ClimbGravity * (Input.GetButton("Jump") ?  1 : LowJumpFactor) - 1) * Time.fixedDeltaTime;
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

        // apply movement to player
        rb.velocity = new Vector2(XVel, rb.velocity.y);
    }

    private void ApplyJumpForce()
    {
        if (OnGround)
        {
            rb.velocity = Vector2.up * JumpStrength;
            CoyoteTimeAvailable = false;
            CoyoteTimeActive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckBelow())
        {
            // set a flag for coyote time
            CoyoteTimeAvailable = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckBelow())
        {
            // set a flag for coyote time
            CoyoteTimeAvailable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // wait a second for coyote time
        if (CoyoteTimeAvailable) 
        {
            CoyoteTimer = CoyoteTimeDuration;
            CoyoteTimeActive = true;
            CoyoteTimeAvailable = false;
        }
    }

    private bool CheckBelow()
    {
        float boxHeight = 0.05f;
        float spacing = 0.05f;
        Vector2 position = new Vector2(transform.position.x + (FacingRight ? ((BoxCollider2D) coll).offset.x : -((BoxCollider2D) coll).offset.x), transform.position.y + ((BoxCollider2D) coll).offset.y - ((BoxCollider2D) coll).size.y / 2f - boxHeight / 2f);
        Vector2 size = new Vector2(((BoxCollider2D) coll).size.x - spacing, boxHeight);
        Collider2D[] detected = Physics2D.OverlapBoxAll(position, size, 0f);
        
        foreach (Collider2D collider in detected)
        {
            if (collider.tag == "Ground" && collider.gameObject.layer == gameObject.layer)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsGrounded()
    {
        if (!CoyoteTimeActive)
        {
            return CheckBelow();
        }
        else
        {
            return true;
        }
    }

    // private void OnDrawGizmos() 
    // {
    //     float boxHeight = 0.05f;
    //     Vector2 position = new Vector2(transform.position.x + (FacingRight ? ((BoxCollider2D) coll).offset.x : -((BoxCollider2D) coll).offset.x), transform.position.y + ((BoxCollider2D) coll).offset.y - ((BoxCollider2D) coll).size.y / 2f - boxHeight / 2f);
    //     Vector2 size = new Vector2(((BoxCollider2D) coll).size.x - 0.05f, boxHeight);
    //     Gizmos.DrawCube((Vector3) position, (Vector3) size);
    // }

}
