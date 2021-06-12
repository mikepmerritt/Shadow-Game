using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Various component references
    public GameObject Player, Shadow;
    private PlayerMovement PlayerMovement, ShadowMovement;
    private LightController LightController;

    // Game state
    private float HorizontalMovement;
    private bool JumpInput;
    private bool HasActiveShadow;

    private void Awake()
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        ShadowMovement = Shadow.GetComponent<PlayerMovement>();
        LightController = Player.GetComponentInChildren<LightController>();

        HasActiveShadow = true;
    }

    private void Update()
    {
        // check input
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        JumpInput = Input.GetButton("Jump");

        if (HasActiveShadow)
        {
            // check distance between two
            float distance = Vector3.Distance(Player.transform.position, Shadow.transform.position);
            LightController.UpdateRadius(distance);

            // remove shadow if it is lost
            if (distance > 5f)
            {
                Destroy(Shadow);
                HasActiveShadow = false;
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement.Move(HorizontalMovement, JumpInput);
        if (HasActiveShadow)
        {
            ShadowMovement.Move(HorizontalMovement, JumpInput);
        }
    }
}
