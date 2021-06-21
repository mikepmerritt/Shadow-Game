using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NewLightController : MonoBehaviour
{

    public GameStateManager Manager;
    public GameObject Player, Shadow;
    public Light2D Light;

    public float MaxRadius, MinRadius;

    // separation event
    public event EventHandler OnSeparate;

    private void Start()
    {
        Manager.OnRespawn += AssignPlayers;
        Manager.OnDeath += RemovePlayers;
    }

    private void Update()
    {
        if (Player != null && Shadow != null)
        {
            Light.transform.position = (Player.transform.position + Shadow.transform.position) / 2f;
            Light.pointLightOuterRadius = Mathf.Max(0f, MaxRadius - Vector3.Distance(Player.transform.position, Shadow.transform.position));

            if (Light.pointLightOuterRadius <= MinRadius)
            {
                OnSeparate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void AssignPlayers(object sender, GameStateManager.OnRespawnArgs e)
    {
        Player = e.Player;
        Shadow = e.Shadow;

        Light.intensity = 1f;
    }

    private void RemovePlayers(object sender, EventArgs e)
    {
        Player = null;
        Shadow = null;

        Light.intensity = 0f;
    }
}
