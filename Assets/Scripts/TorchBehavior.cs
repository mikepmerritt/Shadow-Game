using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchBehavior : MonoBehaviour
{

    public Sprite LitTorch, LitShadowTorch;
    public SpriteRenderer Torch, Shadow;
    public Light2D Light;

    public void LightTorch()
    {
        Torch.sprite = LitTorch;
        Shadow.sprite = LitShadowTorch;

        Light.pointLightOuterRadius = 2f;
    }
}
