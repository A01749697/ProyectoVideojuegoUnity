using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    [SerializeField] AudioSource audioSource; 
    

    void Start()
    {
     /*if(!PlayerPrefs.HasKey("musicVolume"))
     {
     	PlayerPrefs.SetFloat("musicVolume", 1);
     
     } 
      audioSource.volume = volumeSlider.value;
*/     Load();
    }

   public void ChangeVolume(){
    audioSource.volume = volumeSlider.value; 
    Save(); 
   }

   private void Load(){
    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1); 
   }

   private void Save(){
   	PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
   }
}
