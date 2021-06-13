using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{

    private const float ButtonWidth = 0.5f;

    public event EventHandler OnButtonDown;
    public event EventHandler OnButtonUp;

    public bool ButtonActive;

    public Collider2D ButtonCollider;

    private void Update()
    {
        Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, ButtonWidth);

        bool foundObject = false;
        foreach (Collider2D collider in detected)
        {
            if (collider != ButtonCollider && collider.gameObject.tag != "Ground" && collider.gameObject.tag != "Checkpoint")
            {
                if (collider.gameObject.layer == gameObject.layer)
                {
                    foundObject = true;
                }
            }
        }
        if (foundObject && !ButtonActive)
        {
            OnButtonDown?.Invoke(this, EventArgs.Empty);
        }
        else if (!foundObject && ButtonActive)
        {
            OnButtonUp?.Invoke(this, EventArgs.Empty);
        }
        ButtonActive = foundObject;
    }

}
