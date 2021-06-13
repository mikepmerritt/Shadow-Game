using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioClip Jump, Light, Die, Button, Lift, Drop, Stairs;
    public AudioSource Source;
    public Image AudioButton;
    public Sprite AudioOff, AudioOn;

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

    public void ToggleSound()
    {
        if (Source.volume != 0)
        {
            Source.volume = 0;
            AudioButton.sprite = AudioOff;
        }
        else
        {
            Source.volume = 0.5f;
            AudioButton.sprite = AudioOn;
        }
    }
}
