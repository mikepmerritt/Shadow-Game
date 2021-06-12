using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Rigidbody2D CharacterRigidbody;
    private Animator CharacterAnimator;
    private PlayerMovement CharacterMovement;
    public string Name;
    private float XVelocity, YVelocity;
    private bool SpacePressed, OnGround;
    public bool IsInterruptable;

    private void Awake()
    {
        CharacterRigidbody = GetComponent<Rigidbody2D>();
        CharacterAnimator = GetComponent<Animator>();
        CharacterMovement = GetComponent<PlayerMovement>();
        IsInterruptable = true;
    }
    
    private void Update()
    {
        XVelocity = CharacterRigidbody.velocity.x;
        YVelocity = CharacterRigidbody.velocity.y;
        SpacePressed = Input.GetButtonDown("Jump");
        OnGround = CharacterMovement.CheckOnGround();
        
        if(IsInterruptable)
        {
            if(OnGround)
            {
                if(CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(Name + "Fall"))
                {
                    CharacterAnimator.Play(Name + "Land");
                    IsInterruptable = false;
                }
                else if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
                {
                    CharacterAnimator.Play(Name + "Walk");
                }
                else if (SpacePressed)
                {
                    CharacterAnimator.Play(Name + "Jump");
                    IsInterruptable = false;
                }
                else
                {
                    CharacterAnimator.Play(Name + "Idle");
                }
            }
            else
            {
                if(YVelocity > 0)
                {
                    CharacterAnimator.Play(Name + "Rise");
                }
                else if(YVelocity <= 0)
                {
                    CharacterAnimator.Play(Name + "Fall");
                }
            }
        }
    }

    public void AllowInterruption()
    {
        IsInterruptable = true;
    }
}
