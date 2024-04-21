using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    public AudioSource audioSound;
    public void playButton()
    {
    	audioSound.Play();
    }
}