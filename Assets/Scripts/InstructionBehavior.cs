using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InstructionBehavior : MonoBehaviour
{

    public ButtonBehavior ButtonBehavior;
    private Light2D Light;
    private float TargetIntensity;

    private const float MaxIntensity = 0.4f;

    private void Start()
    {
        Light = GetComponentInChildren<Light2D>();
        ButtonBehavior.OnButtonDown += EnableLight;
        ButtonBehavior.OnButtonUp += DisableLight;
        TargetIntensity = Light.intensity;
    }

    private void EnableLight(object sender, EventArgs e)
    {
        TargetIntensity = MaxIntensity;
    }

    private void DisableLight(object sender, EventArgs e)
    {
        TargetIntensity = 0f;
    }

    private void Update()
    {
        if (Light.intensity < TargetIntensity)
        {
            Light.intensity = Mathf.Min(MaxIntensity, Light.intensity + Time.deltaTime);
        }
        else if (Light.intensity > TargetIntensity)
        {
            Light.intensity = Mathf.Max(0f, Light.intensity - Time.deltaTime);
        }
    }
}
