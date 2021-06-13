using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public ButtonBehavior ButtonBehavior;
    public GameObject Child;

    private void Start()
    {
        ButtonBehavior.OnButtonDown += EnableChild;
    }

    private void EnableChild(object sender, EventArgs e)
    {
        Child.SetActive(!Child.activeSelf);
        ButtonBehavior.OnButtonDown -= EnableChild;
    }
}
