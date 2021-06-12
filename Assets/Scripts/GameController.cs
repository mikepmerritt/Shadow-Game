using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public InputController InputController;

    private GameObject Spawnpoint;

    public GameObject PlayerPrefab, ShadowPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void SetSpawnPoint(GameObject checkpoint)
    {
        InputController.RespawnShadow();
        Spawnpoint = checkpoint;
    }

    public void RespawnPlayer()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // // Kill player

        GameObject player = Instantiate(PlayerPrefab, Spawnpoint.transform.position, Quaternion.identity);
        InputController.ReplacePlayer(player);
        InputController.RespawnShadow();

    }
}
