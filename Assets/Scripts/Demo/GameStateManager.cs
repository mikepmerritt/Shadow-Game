using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    // prefabs
    public GameObject PlayerPrefab, ShadowPrefab;

    // event data
    public event EventHandler<OnRespawnArgs> OnRespawn;
    public event EventHandler OnDeath;

    public class OnRespawnArgs : EventArgs
    {
        public GameObject Player;
        public GameObject Shadow;
    }

    // active object information
    public GameObject Checkpoint, Player, Shadow;

    private void Start()
    {
        this.OnDeath += DestroyPlayers;
        this.OnRespawn += RespawnPlayers;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
            OnRespawn?.Invoke(this, new OnRespawnArgs
            {
                Player = Instantiate(PlayerPrefab),
                Shadow = Instantiate(ShadowPrefab)
            });
        }
    }

    private void DestroyPlayers(object sender, EventArgs e)
    {
        Destroy(Player);
        Destroy(Shadow);
    }

    private void RespawnPlayers(object sender, OnRespawnArgs e)
    {
        Player = e.Player;
        Shadow = e.Shadow;
    }
}
