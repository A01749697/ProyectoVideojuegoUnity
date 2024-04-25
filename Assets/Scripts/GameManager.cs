using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public static GameObject[] players;
    public static Player currentPlayer;
    public static bool modoTirarCarta = false;
    public static int currentPlayerIndex = 0;
    public static int diceSideThrown = 0;
    public static bool gameOver = false;
    public TurnAnnouncement TurnAnnouncement;


    public void TirarCarta(){
        if (currentPlayer.hasThrownCard) {
            Debug.Log("Player has already drawn a card this turn.");
            return;
        }
        currentPlayer.hasDrawnCard = true;
    }

    public void DrawCard(){   
        if (currentPlayer.hasDrawnCard) {
            Debug.Log("Player has already drawn a card this turn.");
            return;
        }
  
        System.Random random = new System.Random();
        Cards randCard = null;

        if(currentPlayer.availableCardSlots.All(slot => slot == false)) {
            Debug.Log("All card slots are occupied, cannot draw more.");
            return;
        }
        do {
            randCard = currentPlayer.deck[random.Next(0, currentPlayer.deck.Count)];
        } while(randCard.cardOnHand);

        randCard.cardOnHand = true;
        randCard.player = currentPlayer; 
        for(int i = 0; i < currentPlayer.availableCardSlots.Length; i++){
            if(currentPlayer.availableCardSlots[i]){
                randCard.handIndex = i;
                randCard.transform.position = currentPlayer.cardSlots[i].position;
                randCard.transform.rotation = currentPlayer.cardSlots[i].rotation;
                currentPlayer.availableCardSlots[i] = false;
                break;
            }
        }
        currentPlayer.hasDrawnCard = true;
    }

    public void Start()
    {
        players = new GameObject[4];
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
        players[2] = GameObject.Find("Player3");
        players[3] = GameObject.Find("Player4");

        foreach (GameObject playerObject in players)
        {
            currentPlayer = playerObject.GetComponent<Player>();
            for (int i = 0; i < 3; i++)
            {
                DrawCard();
                currentPlayer.hasDrawnCard = false;
            }
        }

        StartCoroutine(PlayerTurns());
    }

    IEnumerator PlayerTurns()
    {
        while (!gameOver)
        {
            currentPlayer = players[currentPlayerIndex].GetComponent<Player>();
            Debug.Log("Current player has changed to: " + currentPlayer.name); 
            currentPlayer.hasDrawnCard = false;
            currentPlayer.hasRolledDice = false;
            currentPlayer.hasThrownCard = false;
            currentPlayer.hasPlayedCard = false;

            //Display turn
            if (TurnAnnouncement != null){
                TurnAnnouncement.DisplayTurn(currentPlayer.name);
            }
            //Ocult the cards of all the players which are not the current player
            foreach (GameObject playerObject in players)
            {
                Player player = playerObject.GetComponent<Player>();
                foreach (Cards card in player.deck)
                {
                    // If the player is not the current player, hide their cards that are in hand
                    if (player != currentPlayer && card.cardOnHand)
                    {
                        card.gameObject.SetActive(false);
                    }
                    // Otherwise, make sure their cards in hand are visible
                    else if (card.cardOnHand)
                    {
                        card.gameObject.SetActive(true);
                    }
                }
            }


            while (!currentPlayer.hasPlayedCard || !currentPlayer.hasRolledDice || !(currentPlayer.hasThrownCard || currentPlayer.hasDrawnCard))
            { 
                yield return null;
            }

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }
    }

}
