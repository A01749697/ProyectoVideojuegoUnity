using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Cards> deck = new List<Cards>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public bool hasDrawnCard = false;
    public bool hasRolledDice = false;

    public void Start()
    {
        foreach (var card in deck)
        {
            card.player = this;
        }
    }
}