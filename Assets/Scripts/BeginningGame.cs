using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningGame : MonoBehaviour
{
    public GameObject BotonesUI;
    public GameObject PanelesUI;
    public GameObject panelBeginningGame;
    public void Awake()
    {
        BotonesUI.SetActive(false);
        PanelesUI.SetActive(false);
    }

    public void botonCoyoteFinance()
    {
        BotonesUI.SetActive(true);
        PanelesUI.SetActive(true);
        panelBeginningGame.SetActive(false);
        GameManager.instance.finanzeChosen = true;
        GameManager.instance.players[0].GetComponent<Player>().finanzePlayer = 0;
    }
        public void botonBancoFinance()
    {
        BotonesUI.SetActive(true);
        PanelesUI.SetActive(true);
        panelBeginningGame.SetActive(false);
        GameManager.instance.finanzeChosen = true;
        GameManager.instance.players[0].GetComponent<Player>().finanzePlayer = 1;
    }

    public void botonVerqorFinance()
    {
        BotonesUI.SetActive(true);
        PanelesUI.SetActive(true);
        panelBeginningGame.SetActive(false);
        GameManager.instance.finanzeChosen = true;
        GameManager.instance.players[0].GetComponent<Player>().finanzePlayer = 2;
    }





}
