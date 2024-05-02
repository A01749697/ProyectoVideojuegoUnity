using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class HudGame : MonoBehaviour
{
    //Instacia estatica de tipo HUD
    public static HudGame instance;

    //Declarar una referencia a un objeto de tipo TextMesh pro input field
    public TMP_Text PLAYER1informationUI;
    public TMP_Text PLAYER2informationUI;
    public TMP_Text PLAYER3informationUI;
    public TMP_Text PLAYER4informationUI;

    public TMP_Text player1Cultivos;
    public TMP_Text player2Cultivos;
    public TMP_Text player3Cultivos;
    public TMP_Text player4Cultivos;

    //Hacer referencia a un elemento Panel-UI
    public GameObject panelUI;

    //Hacer 
    public GameObject player2PlagueMODE;
    public GameObject player3PlagueMODE;
    public GameObject player4PlagueMODE;
    public GameObject getOutButtonPLAGUE;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePlayerInformationUI(int indexPlayer, int pathIndex)
    {
        if(indexPlayer==1){
            PLAYER1informationUI.text = pathIndex + "/24";
        }
        if(indexPlayer==2){
            PLAYER2informationUI.text = pathIndex + "/24";
        }
        if(indexPlayer==3){
            PLAYER3informationUI.text = pathIndex + "/24";
        }
        if(indexPlayer==4){
            PLAYER4informationUI.text = pathIndex + "/24";
        }
    }

    public void UpdatePlayerCultivosUI(int indexPlayer, int numCultivos)
    {
        if(indexPlayer==1){
            player1Cultivos.text = numCultivos + "/4";
        }
        if(indexPlayer==2){
            player2Cultivos.text = numCultivos + "/4";
        }
        if(indexPlayer==3){
            player3Cultivos.text = numCultivos + "/4";
        }
        if(indexPlayer==4){
            player4Cultivos.text = numCultivos + "/4";
        }
    }

    //Funcion que hace aparecer el panel de UI, y tambien a aquellos jugadores que esten
    // en el vector(que recibe como parametro esta funcion)
    public void ShowPanelUIPlagueMode(List<int> players)
    {
        panelUI.SetActive(true);
        foreach (var player in players)
        {
            if(player == 2)
            {
                player2PlagueMODE.SetActive(true);
            }
            if(player == 3)
            {
                player3PlagueMODE.SetActive(true);
            }
            if(player == 4)
            {
                player4PlagueMODE.SetActive(true);
            }
        }
    }

    //Funcion que esconde el panel de UI, y tambien al resto de jugadores
    public void HidePanelUIPlagueMode()
    {
        panelUI.SetActive(false);
        player2PlagueMODE.SetActive(false);
        player3PlagueMODE.SetActive(false);
        player4PlagueMODE.SetActive(false);
    }

    public void Player2PlagueOnCrops()
    {
        //Si el jugador 2 tiene cultivo con proteccion igual a 0 entonces se le resta un cultivo
        if(GameManager.instance.players[1].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] == 0){
            GameManager.instance.players[1].GetComponent<Player>().numberCultivos--;
            //Cultivo NO disponoble
            GameManager.instance.players[1].GetComponent<Player>().cultivosAvailable[GameManager.instance.lastPlagueCardPlayed.cardColor] = false;
            //Actualizar el texto de los cultivos
            UpdatePlayerCultivosUI(2, GameManager.instance.players[1].GetComponent<Player>().numberCultivos);
        }else{
            //Si el jugador 2 tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
            GameManager.instance.players[1].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] -= 1;
        }
    }

    public void Player3PlagueOnCrops()
    {
        //Si el jugador 3 tiene cultivo con proteccion igual a 0 entonces se le resta un cultivo
        if(GameManager.instance.players[2].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] == 0){
            GameManager.instance.players[2].GetComponent<Player>().numberCultivos--;
            //Cultivo NO disponoble
            GameManager.instance.players[2].GetComponent<Player>().cultivosAvailable[GameManager.instance.lastPlagueCardPlayed.cardColor] = false;
            //Actualizar el texto de los cultivos
            UpdatePlayerCultivosUI(3, GameManager.instance.players[2].GetComponent<Player>().numberCultivos);
        }else{
            //Si el jugador 3 tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
            GameManager.instance.players[2].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] -= 1;
        }
    }

    public void Player4PlagueOnCrops()
    {
        if(GameManager.instance.players[3].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] == 0){
            GameManager.instance.players[3].GetComponent<Player>().numberCultivos--;
            //Cultivo NO disponoble
            GameManager.instance.players[3].GetComponent<Player>().cultivosAvailable[GameManager.instance.lastPlagueCardPlayed.cardColor] = false;
            //Actualizar el texto de los cultivos
            UpdatePlayerCultivosUI(4, GameManager.instance.players[3].GetComponent<Player>().numberCultivos);
        }else{  
            GameManager.instance.players[3].GetComponent<Player>().cultivosProtection[GameManager.instance.lastPlagueCardPlayed.cardColor] -= 1;
        }
    }

}
