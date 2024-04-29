using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Cards> deck = new List<Cards>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    //Jugador ha dibujado una carta
    public bool hasDrawnCard = false;
    //Jugador ha lanzado una carta
    public bool hasThrownCard = false;
    //Jugador ha lanzado los dados
    public bool hasRolledDice = false;
    //Jugador ha jugado una carta
    public bool hasPlayedCard = false;

    //crear un vector de tamaño 4 para guardar booleanos sobre cultivos
    public bool[] cultivosAvailable = new bool[4];

    public int numberCultivos = 0;
    public void Start()
    {
        foreach (var card in deck)
        {
            card.player = this;
        }
    }

    
}