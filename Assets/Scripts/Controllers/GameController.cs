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

    public List<GameObject> Boxes;
    public List<Vector3> BoxStarts;
    public GameObject BoxPrefab;
    public Animator RespawnInstructions;
    private bool IsTextOnScreen;

    private void Start()
    {
        foreach (GameObject box in Boxes)
        {
            BoxStarts.Add(box.transform.position);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsAlive)
        {
            RespawnPlayer();
            ReloadBoxes();
        }

        if (IsAlive && (GameObject.FindGameObjectWithTag("Player").transform.position.y < -20f
            || GameObject.FindGameObjectWithTag("Shadow").transform.position.y < -20f))
        {
            IsAlive = false;
        }

        if (!IsAlive && !IsTextOnScreen)
        {
            RespawnInstructions.SetTrigger("Appear");
            IsTextOnScreen = true;
        }
        else if (IsAlive && IsTextOnScreen)
        {
            RespawnInstructions.SetTrigger("Disappear");
            IsTextOnScreen = false;
        }
    }

    public void SetSpawnPoint(GameObject checkpoint)
    {
        if (!checkpoint.GetComponent<TorchBehavior>().IsLit())
        {
            checkpoint.GetComponent<TorchBehavior>().LightTorch();
            if (Spawnpoint != null) 
            {
                Spawnpoint.GetComponent<TorchBehavior>().UnlightTorch();
            }
            InputController.RespawnShadow();
            Spawnpoint = checkpoint;
    
            GameObject shadow = GameObject.FindGameObjectWithTag("Shadow");
            // CameraController.SetShadow(shadow);
            LightController.SetShadow(shadow);
        }
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

        IsAlive = true;
    }

    public void ReloadBoxes()
    {
        // clear old boxes from list
        while (Boxes.Count != 0) 
        {
            GameObject lostBox = Boxes[0];
            Boxes.RemoveAt(0);
            Destroy(lostBox);
        }

        // clear any stray boxes
        GameObject[] strayBoxes = GameObject.FindGameObjectsWithTag("Carryable");
        for (int i = 0; i < strayBoxes.Length; i++)
        {
            Destroy(strayBoxes[i]);
        }

        // repopulate boxes
        foreach (Vector3 position in BoxStarts)
        {
            Boxes.Add(Instantiate(BoxPrefab, position, Quaternion.identity));
        }
    }
}
