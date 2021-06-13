using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public InputController InputController;
    // public CameraController CameraController;
    public LightController LightController;

    private GameObject Spawnpoint;

    public GameObject PlayerPrefab, ShadowPrefab;
    public bool IsAlive = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsAlive)
        {
            RespawnPlayer();
        }
    }

    public void SetSpawnPoint(GameObject checkpoint)
    {
        InputController.RespawnShadow();
        Spawnpoint = checkpoint;
  
        GameObject shadow = GameObject.FindGameObjectWithTag("Shadow");
        // CameraController.SetShadow(shadow);
        LightController.SetShadow(shadow);
    }

    public void RespawnPlayer()
    {
        // CameraController.StopCamera();
        LightController.Paused = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Shadow"));
        InputController.HasActiveShadow = false;

        GameObject player = Instantiate(PlayerPrefab, Spawnpoint.transform.position, Quaternion.identity);
        InputController.ReplacePlayer(player);
        InputController.RespawnShadow();
        InputController.StartInput();

        GameObject shadow = GameObject.FindGameObjectWithTag("Shadow");

        // CameraController.SetPlayer(player);
        // CameraController.SetShadow(shadow);
        // CameraController.StartCamera();

        LightController.ActivateLight();
        LightController.SetPlayer(player);
        LightController.SetShadow(shadow);
        LightController.Paused = false;
    }
}
