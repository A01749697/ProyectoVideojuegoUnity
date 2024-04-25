using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HudGame : MonoBehaviour
{
    //Instacia estatica de tipo HUD
    public static HudGame instance;

    //Declarar una referencia a un objeto de tipo TextMesh pro input field
    public TMP_Text PLAYER1informationUI;
    public TMP_Text PLAYER2informationUI;
    public TMP_Text PLAYER3informationUI;
    public TMP_Text PLAYER4informationUI;
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

}
