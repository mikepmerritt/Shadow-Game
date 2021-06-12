using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{

    public GameObject Player, Shadow;
    public Light2D Light;
    private bool IsFading, LightOut;
    public bool Paused = false;

    public void Awake()
    {
        LightOut = false;
    }

    public void UpdateRadius(float mangitude)
    {
        if (!IsFading)
        {
            Light.pointLightOuterRadius = Mathf.Max(8 - mangitude);

            if (Light.pointLightOuterRadius < 3f)
            {
                FadeOut();
            }
        }
    }

    public void FadeOut()
    {
        // kill light over time
        IsFading = true;
    }

    private void Update()
    {
        if (!LightOut && !Paused && !(Player == null || Shadow == null))
        {
            float xDisplacement = Mathf.Abs(Player.transform.position.x - Shadow.transform.position.x);
            float xMid = Mathf.Min(Player.transform.position.x, Shadow.transform.position.x) + xDisplacement / 2f;

            float yDisplacement = Mathf.Abs(Player.transform.position.y - Shadow.transform.position.y);
            float yMid = Mathf.Min(Player.transform.position.y, Shadow.transform.position.y) + yDisplacement / 2f;

            transform.position = new Vector3(xMid, yMid, 0f);

            if (IsFading)
            {
                Light.pointLightOuterRadius = Mathf.Max(0, Light.pointLightOuterRadius - Time.fixedDeltaTime * 2f);
                if (Light.pointLightOuterRadius <= 0)
                {
                    LightOut = true;
                    Destroy(Player);
                    Destroy(Shadow);
                    FindObjectOfType<InputController>().HasActiveShadow = false;
                    FindObjectOfType<GameController>().IsAlive = false;
                    FindObjectOfType<CameraController>().StopCamera();
                }
            }
        }
    }

    public void ActivateLight()
    {
        IsFading = false;
        LightOut = false;
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }

    public void SetShadow(GameObject shadow)
    {
        Shadow = shadow;
    }
}
