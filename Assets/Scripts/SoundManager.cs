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
*/  Load();
    }

   public void ChangeVolume(){
    audioSource.volume = volumeSlider.value; //valor del volumen= valor del slider //AudioListener es global, volumen de cada audio source
    Save(); 
   }

   private void Load(){
    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1); 
   }

   private void Save(){
   	//playerpref preferencias del jugador en la sección de configuración del juego
   	PlayerPrefs.SetFloat(/*KeyName*/"musicVolume", /*Valor*/ volumeSlider.value);
   }
}
