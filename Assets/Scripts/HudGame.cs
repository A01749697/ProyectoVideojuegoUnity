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
        //imprimri algo
        Debug.Log("AAAAAAAAAAAAA has already thrown a card this turn.");
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

}
