﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Various component references
    public GameObject Player, Shadow;
    private PlayerMovement PlayerMovement, ShadowMovement;
    private LightController LightController;
    public GameObject ShadowPrefab;
    public GameController GameController;
    public CameraController CameraController;

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
        JumpInput = Input.GetButtonDown("Jump");

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

        if (HasActiveShadow)
        {
            PlayerMovement.Move(HorizontalMovement, JumpInput);
            ShadowMovement.Move(HorizontalMovement, JumpInput);
        }
        else
        {
            PlayerMovement.Stop();
            CameraController.StopCamera();
        }
    }

    public void RespawnShadow()
    {
        if (HasActiveShadow)
        {
            Shadow.transform.position = new Vector3(Player.transform.position.x + 0.25f, Player.transform.position.y + 0.25f, 0f);
        }
        else 
        {
            Shadow = Instantiate(ShadowPrefab, new Vector3(Player.transform.position.x + 0.25f, Player.transform.position.y + 0.25f, 0f), Quaternion.identity);
            ShadowMovement = Shadow.GetComponent<PlayerMovement>();
            HasActiveShadow = true;
            LightController.ActivateLight();
        }
        Shadow.GetComponent<Animator>().Play("ShadowRespawn");
        Shadow.GetComponent<CharacterAnimation>().IsInterruptable = false;
    }

    public void ReplacePlayer(GameObject newPlayer)
    {
        Player = newPlayer;
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        LightController = Player.GetComponentInChildren<LightController>();
    }

}
