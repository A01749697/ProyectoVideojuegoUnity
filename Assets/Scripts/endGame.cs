using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    //hacer referencia a un objeto UI
    public GameObject endGameUI;
    //Hacer referencia a un boton
    public GameObject buttonMenu;
    public GameObject botonesUI;

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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
