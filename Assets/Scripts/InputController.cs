using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InputController : MonoBehaviour
{
    public GameObject Player, Shadow;
    private PlayerMovement PlayerMovement, ShadowMovement;

    private float HorizontalMovement;
    private bool JumpInput;

    public Light2D Light;

    private void Awake()
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        ShadowMovement = Shadow.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // check input
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetButton("Jump");

        // check distance between two
        Light.pointLightOuterRadius = Mathf.Max(0, 8 - Vector3.Distance(Player.transform.position, Shadow.transform.position));
    }

    private void FixedUpdate()
    {
        PlayerMovement.Move(HorizontalMovement, JumpInput);
        ShadowMovement.Move(HorizontalMovement, JumpInput);
    }
}
