using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchBehavior : MonoBehaviour
{

    public Sprite LitTorch, LitShadowTorch;
    public SpriteRenderer Torch, Shadow;
    public Light2D Light;

    private Sprite UnlitTorch, UnlitShadowTorch;
    private float TargetRadius;
    private bool Lit;

    private void Start()
    {
        UnlitTorch = Torch.sprite;
        UnlitShadowTorch = Shadow.sprite;
    }

    public void LightTorch()
    {
        Torch.sprite = LitTorch;
        Shadow.sprite = LitShadowTorch;

        TargetRadius = 2f;

        Lit = true;
        FindObjectOfType<SoundController>().PlayLight();
    }

    public void UnlightTorch()
    {
        Torch.sprite = UnlitTorch;
        Shadow.sprite = UnlitShadowTorch;

        TargetRadius = 0f;

        Lit = false;
    }

    public bool IsLit()
    {
        return Lit;
    }

    private void Update()
    {
        if (Light.pointLightOuterRadius < TargetRadius)
        {
            Light.pointLightOuterRadius = Mathf.Min(TargetRadius, Light.pointLightOuterRadius + 3f * Time.deltaTime);
        }
        else if (Light.pointLightOuterRadius > TargetRadius)
        {
            Light.pointLightOuterRadius = Mathf.Max(TargetRadius, Light.pointLightOuterRadius - 3f * Time.deltaTime);
        }
    }
}
