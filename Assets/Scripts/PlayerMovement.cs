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
    private bool OnGround = true;
    private bool FacingRight = true;

    // Components and references
    public Rigidbody2D rb;
    public Collider2D playerCollider;
    public GameObject floorCheck;
    public LayerMask GroundLayer;

    public void Move(float horizontalMovement, bool jumpInput)
    {
        float xVelocity = rb.velocity.x, yVelocity = rb.velocity.y;

        Collider2D[] detected = Physics2D.OverlapCircleAll(floorCheck.transform.position, PlayerWidth, GroundLayer);

        //Debug.Log(detected.Length);

        bool foundGround = false;
        foreach (Collider2D collider in detected)
        {
            if (collider != playerCollider)
            {
                foundGround = true;
            }
        }
        OnGround = foundGround;

        //Debug.Log(gameObject.name + OnGround);

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

        if (xVelocity < -0.0001f && FacingRight && horizontalMovement < 0 || xVelocity > 0.0001f && !FacingRight && horizontalMovement > 0)
        {
            FacingRight = !FacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
    
    public void Stop()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            TorchBehavior torch = collision.gameObject.GetComponent<TorchBehavior>();
            torch.LightTorch();

            FindObjectOfType<GameController>().SetSpawnPoint(torch.gameObject);
        }
        else if (collision.tag == "Finish")
        {
            Debug.Log("Door.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hazard")
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        Stop();
        if (gameObject.tag == "Player")
        {
            // player death anim
            GetComponent<Animator>().Play("WizardHit");
            GetComponent<CharacterAnimation>().IsInterruptable = false;
            FindObjectOfType<LightController>().FadeOut();
            // FindObjectOfType<CameraController>().StopCamera();
            GameObject.FindGameObjectWithTag("Shadow").GetComponent<Animator>().Play("ShadowHit");
            GameObject.FindGameObjectWithTag("Shadow").GetComponent<CharacterAnimation>().IsInterruptable = false;
        }
        else 
        {
            // shadow death anim
            GetComponent<Animator>().Play("ShadowHit");
            GetComponent<CharacterAnimation>().IsInterruptable = false;
            FindObjectOfType<LightController>().FadeOut();
            // FindObjectOfType<CameraController>().StopCamera();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("WizardHit");
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAnimation>().IsInterruptable = false;
        }
        FindObjectOfType<InputController>().StopInput();
    }

    public bool CheckOnGround()
    {
        return OnGround;
    }
}
