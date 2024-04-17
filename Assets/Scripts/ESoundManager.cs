using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESoundManager : MonoBehaviour
{
    [SerializeField] Slider effectSlider; 
    [SerializeField] AudioSource[] soundEffectSources; 

   
    void Start()
    {
     /*if(!PlayerPrefs.HasKey("musicVolume"))
     {
        PlayerPrefs.SetFloat("musicVolume", 1);
     
     } 
      audioSource.volume = volumeSlider.value;*/  
      Load();
    }

    public void ChangeEffectVolume()
    {
        foreach (AudioSource source in soundEffectSources)
        {
            source.volume = effectSlider.value;
        }
        Save();
    }

    private void Load()
    {
        effectSlider.value = PlayerPrefs.GetFloat("effectVolume", 1);
        ChangeEffectVolume(); 
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("effectVolume", effectSlider.value);
    }
}
