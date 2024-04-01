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
    public static int currentPlayerIndex = 0;
    public static int diceSideThrown = 0;
    public static bool gameOver = false;

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

    void Start()
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
            currentPlayer.hasDrawnCard = false;
            currentPlayer.hasRolledDice = false;

            while (!currentPlayer.hasDrawnCard || !currentPlayer.hasRolledDice)
            {

                yield return null; // wait for next frame
            }

            // Move to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }
    }

}
