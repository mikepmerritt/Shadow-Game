using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub : MonoBehaviour
{

    public ButtonBehavior ButtonBehavior;

    // Start is called before the first frame update
    void Start()
    {
        ButtonBehavior.OnButtonDown += ButtonDown;
        ButtonBehavior.OnButtonUp += ButtonUp;
    }

    private void ButtonUp(object sender, EventArgs e)
    {
        Debug.Log("Button Up");
    }

    private void ButtonDown(object sender, EventArgs e)
    {
        Debug.Log("Button Down");
    }
}
