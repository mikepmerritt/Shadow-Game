using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTorch : MonoBehaviour
{
    public static EventHandler<OnTorchLitArgs> OnTorchLit;
    
    public class OnTorchLitArgs
    {
        public GameObject ActiveTorch;
    }

    public bool IsLit;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsLit && collision.tag == "Player")
        {
            OnTorchLit?.Invoke(this, new OnTorchLitArgs
            {
                ActiveTorch = gameObject
            });
            IsLit = true;
        }
    }

    public void ActivateTorch()
    {
        // enable light and start animation for active
        Debug.Log(gameObject.name + " is on.");
        ani.Play("TorchLit");
    }

    public void DisableTorch()
    {
        // kill light and start extinguish animation
        Debug.Log(gameObject.name + " is off.");
        ani.Play("TorchUnlit");
        IsLit = false;
    }
}
