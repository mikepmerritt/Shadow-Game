using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip Jump, Light, Die, Button, Lift, Drop, Stairs;
    public AudioSource Source;

    public void PlayJump()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Jump);
        }
        else
        {
            Source.PlayOneShot(Jump);
        }
    }

    public void PlayLight()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Jump);
        }
        else
        {
            Source.PlayOneShot(Light);
        }
    }

    public void PlayDie()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Die);
        }
        else
        {
            Source.PlayOneShot(Die);
        }
    }

    public void PlayButton()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Button);
        }
        else
        {
            Source.PlayOneShot(Button);
        }

    }

    public void PlayLift()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Lift);
        }
        else
        {
            Source.PlayOneShot(Lift);
        }
    }

    public void PlayDrop()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Drop);
        }
        else
        {
            Source.PlayOneShot(Drop);
        }
    }

    public void PlayStairs()
    {
        if (Source.isPlaying)
        {
            Source.Stop();
            Source.PlayOneShot(Stairs);
        }
        else
        {
            Source.PlayOneShot(Stairs);
        }
    }
}
