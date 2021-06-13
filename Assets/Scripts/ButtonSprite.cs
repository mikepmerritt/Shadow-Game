using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSprite : MonoBehaviour
{
    public Sprite Up, Down;
    private SpriteRenderer ButtonRenderer;
    public ButtonBehavior ButtonBehavior;

    void Start()
    {
        ButtonRenderer = GetComponent<SpriteRenderer>();
        ButtonBehavior.OnButtonDown += ButtonDown;
        // ButtonBehavior.OnButtonUp += ButtonUp;
    }

    private void ButtonUp(object sender, EventArgs e)
    {
        ButtonRenderer.sprite = Up;
    }

    private void ButtonDown(object sender, EventArgs e)
    {
        ButtonRenderer.sprite = Down;
        ButtonBehavior.OnButtonDown -= ButtonDown;
    }
}
