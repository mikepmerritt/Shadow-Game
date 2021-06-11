using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject Player, Shadow;
    private PlayerMovement PlayerMovement, ShadowMovement;

    private float HorizontalMovement;
    private bool JumpInput;

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
        Debug.Log(Vector3.Distance(Player.transform.position, Shadow.transform.position));
    }

    private void FixedUpdate()
    {
        PlayerMovement.Move(HorizontalMovement, JumpInput);
        ShadowMovement.Move(HorizontalMovement, JumpInput);
    }
}
