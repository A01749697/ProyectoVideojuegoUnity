using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class endGame : MonoBehaviour
{
    //hacer referencia a un objeto UI
    public GameObject endGameUI;
    //Hacer referencia a un boton
    public GameObject buttonMenu;
    public GameObject botonesUI;
    public GameObject goVerqorBootton;

    public static endGame instance;
    public GameObject menuPausa;
    public GameObject panelesUI;


    //Cduando el juego termine se activa el UI
    public void EndGameMoment()
    {
        //Desactivar los botones de la UI
        botonesUI.SetActive(false);
        //Desactivar el panel de la UI
        panelesUI.SetActive(false);
        //Descativar el panel de pausa
        menuPausa.SetActive(false);
        //Activar el panel de fin de juego
        endGameUI.SetActive(true);
    }

    //Cuando el boton sea presionado se carga la escena 0
    public void BackMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void goVerqorWebsite()
    {
        StartCoroutine(verqorSiteWeb());
    }
    IEnumerator verqorSiteWeb(){
        UnityWebRequest request = UnityWebRequest.Get("/indexGAMe.html");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success) {
            string respuesta = request.downloadHandler.text;
            print("Respuesta: " + respuesta);
        } else {
            print("Error: " + request.error);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
