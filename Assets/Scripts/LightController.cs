using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D Light;
    private bool IsFading, LightOut;

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
        if (IsFading)
        {
            Light.pointLightOuterRadius = Mathf.Max(0, Light.pointLightOuterRadius - Time.fixedDeltaTime * 2f);
            if (Light.pointLightOuterRadius <= 0)
            {
                LightOut = true;
            }
        }
    }

    public void ActivateLight()
    {
        IsFading = false;
        LightOut = false;
    }
}
