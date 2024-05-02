using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    //Instacia estatica de tipo GameManager
    public static GameManager instance;
    public Dice dice;
    public GameObject[] players;
    //Hacer referencia a 4 Game Objects que representan a los cultivos
    public GameObject[] cultivos;
    public  Player currentPlayer;
    public Cards lastPlagueCardPlayed;
    public  bool modoTirarCarta = false;
    public int currentPlayerIndex = 0;
    public int diceSideThrown = 0;
    public bool gameOver = false;
    public TurnAnnouncement TurnAnnouncement;
    public void TirarCarta(){
        if (currentPlayer.hasThrownCard || currentPlayer.hasPlayedCard) {
            Debug.Log("Player has already thrown a card this turn.");
            return;
        }
        modoTirarCarta = !modoTirarCarta;
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
        //Actulizar la mano del jugador
        currentPlayer.hand[randCard.handIndex] = randCard;
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

            while (!currentPlayer.hasDrawnCard || !currentPlayer.hasRolledDice || !(currentPlayer.hasThrownCard || currentPlayer.hasPlayedCard))
            {
                //If the current player is different from the player 1
                if (currentPlayerIndex != 0)
                {
                    //!(currentPlayer.hasThrownCard || currentPlayer.hasPlayedCard)
                    if (!(currentPlayer.hasThrownCard || currentPlayer.hasPlayedCard))
                    {             
                        int cardAction = UnityEngine.Random.Range(0, 3);
                        Cards card = currentPlayer.hand[cardAction];
                        if (UnityEngine.Random.Range(0, 2) == 0)
                        {
                            //Si la carta es de tipo Plaga
                            if(card.tipoCarta==1){
                                List<int> playersWithCrops = new List<int>();
                                switch (card.cardColor){
                                    case 0:
                                        //Hacer una lista con aquellos jugadores que tengan cultivos del mismo color que la plaga
                                        playersWithCrops.Clear();
                                        foreach (var playerObject in players) {
                                            Player jugador = playerObject.GetComponent<Player>();
                                            if (jugador.cultivosAvailable[0] && jugador != currentPlayer) {
                                                if(jugador.cultivosProtection[0] != 2){
                                                    playersWithCrops.Add(jugador.playerIndex);
                                                }
                                            }
                                        }
                                        if (playersWithCrops.Count > 0) {       
                                            //Select a random player from the list of players with crops
                                            int randomPlayerIndex = playersWithCrops[UnityEngine.Random.Range(0, playersWithCrops.Count)];
                                            //Imprimir el index del jugador seleccionado
                                            Debug.Log("Player index selected: " + randomPlayerIndex);
                                            if(players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] == 0){
                                                players[randomPlayerIndex-1].GetComponent<Player>().numberCultivos--;
                                                //Cultivo NO disponoble
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosAvailable[card.cardColor] = false;
                                                //Actualizar el texto de los cultivos
                                                HudGame.instance.UpdatePlayerCultivosUI(randomPlayerIndex, players[randomPlayerIndex].GetComponent<Player>().numberCultivos);
                                            }else{
                                                //Si el jugador tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] -= 1;
                                            }
                                            //El jugador actual ha jugado una carta
                                            currentPlayer.hasPlayedCard = true;
                                            currentPlayer.availableCardSlots[cardAction] = true;
                                            card.cardOnHand = false;
                                            //Imprimir que se jugo una carta de tiop plaga, y el color de la plaga
                                            Debug.Log("Player " + currentPlayer.name + " played a plague card of color " + card.cardColor);
                                        }
                                        break;
                                    case 1:
                                        //Hacer una lista con aquellos jugadores que tengan cultivos del mismo color que la plaga
                                        playersWithCrops.Clear();
                                        foreach (var playerObject in players) {
                                            Player jugador = playerObject.GetComponent<Player>();
                                            if (jugador.cultivosAvailable[1] && jugador != currentPlayer) {
                                                if(jugador.cultivosProtection[1] != 2){
                                                    playersWithCrops.Add(jugador.playerIndex);
                                                }
                                            }
                                        }
                                        if (playersWithCrops.Count > 0) {       
                                            //Select a random player from the list of players with crops
                                            int randomPlayerIndex = playersWithCrops[UnityEngine.Random.Range(0, playersWithCrops.Count)];
                                            //Imprimir el index del jugador seleccionado
                                            Debug.Log("Player index selected: " + randomPlayerIndex);
                                            if(players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] == 0){
                                                players[randomPlayerIndex-1].GetComponent<Player>().numberCultivos--;
                                                //Cultivo NO disponoble
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosAvailable[card.cardColor] = false;
                                                //Actualizar el texto de los cultivos
                                                HudGame.instance.UpdatePlayerCultivosUI(randomPlayerIndex, players[randomPlayerIndex-1].GetComponent<Player>().numberCultivos);
                                            }else{
                                                //Si el jugador tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] -= 1;
                                            }
                                            //El jugador actual ha jugado una carta
                                            currentPlayer.hasPlayedCard = true;
                                            currentPlayer.availableCardSlots[cardAction] = true;
                                            card.cardOnHand = false;
                                            //Imprimir que se jugo una carta de tiop plaga, y el color de la plaga
                                            Debug.Log("Player " + currentPlayer.name + " played a plague card of color " + card.cardColor);
                                        }
                                        break;
                                    case 2:
                                        //Hacer una lista con aquellos jugadores que tengan cultivos del mismo color que la plaga
                                        playersWithCrops.Clear();
                                        foreach (var playerObject in players) {
                                            Player jugador = playerObject.GetComponent<Player>();
                                            if (jugador.cultivosAvailable[2] && jugador != currentPlayer) {
                                                if(jugador.cultivosProtection[2] != 2){
                                                    playersWithCrops.Add(jugador.playerIndex);
                                                }
                                            }
                                        }
                                        if (playersWithCrops.Count > 0) {       
                                            //Select a random player from the list of players with crops
                                            int randomPlayerIndex = playersWithCrops[UnityEngine.Random.Range(0, playersWithCrops.Count)];
                                            //Imprimir el index del jugador seleccionado
                                            Debug.Log("Player index selected: " + randomPlayerIndex);
                                            if(players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] == 0){
                                                players[randomPlayerIndex-1].GetComponent<Player>().numberCultivos--;
                                                //Cultivo NO disponoble
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosAvailable[card.cardColor] = false;
                                                //Actualizar el texto de los cultivos
                                                HudGame.instance.UpdatePlayerCultivosUI(randomPlayerIndex, players[randomPlayerIndex].GetComponent<Player>().numberCultivos);
                                            }else{
                                                //Si el jugador tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] -= 1;
                                            }
                                            //El jugador actual ha jugado una carta
                                            currentPlayer.hasPlayedCard = true;
                                            currentPlayer.availableCardSlots[cardAction] = true;
                                            card.cardOnHand = false;
                                            //Imprimir que se jugo una carta de tiop plaga, y el color de la plaga
                                            Debug.Log("Player " + currentPlayer.name + " played a plague card of color " + card.cardColor);
                                        }
                                        break;
                                    case 3:
                                    //Hacer una lista con aquellos jugadores que tengan cultivos del mismo color que la plaga
                                        playersWithCrops.Clear();
                                        foreach (var playerObject in players) {
                                            Player jugador = playerObject.GetComponent<Player>();
                                            if (jugador.cultivosAvailable[3] && jugador != currentPlayer) {
                                                if(jugador.cultivosProtection[3] != 2){
                                                    playersWithCrops.Add(jugador.playerIndex);
                                                }
                                            }
                                        }
                                        if (playersWithCrops.Count > 0) {       
                                            //Select a random player from the list of players with crops
                                            int randomPlayerIndex = playersWithCrops[UnityEngine.Random.Range(0, playersWithCrops.Count)];
                                            //Imprimir el index del jugador seleccionado
                                            Debug.Log("Player index selected: " + randomPlayerIndex);
                                            if(players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] == 0){
                                                players[randomPlayerIndex-1].GetComponent<Player>().numberCultivos--;
                                                //Cultivo NO disponoble
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosAvailable[card.cardColor] = false;
                                                //Actualizar el texto de los cultivos
                                                HudGame.instance.UpdatePlayerCultivosUI(randomPlayerIndex, players[randomPlayerIndex].GetComponent<Player>().numberCultivos);
                                            }else{
                                                //Si el jugador tiene cultivo con proteccion mayor a 0 entonces se le resta 1 a la proteccion
                                                players[randomPlayerIndex-1].GetComponent<Player>().cultivosProtection[card.cardColor] -= 1;
                                            }
                                            //El jugador actual ha jugado una carta
                                            currentPlayer.hasPlayedCard = true;
                                            currentPlayer.availableCardSlots[cardAction] = true;
                                            card.cardOnHand = false;
                                            //Imprimir que se jugo una carta de tiop plaga, y el color de la plaga
                                            Debug.Log("Player " + currentPlayer.name + " played a plague card of color " + card.cardColor);
                                        }
                                        break;
                                }
                            }else{
                                card.OnMouseDown();
                                //Imprimir que tipo de carta se jugo y su coloe
                                Debug.Log("Player " + currentPlayer.name + " played a card of type " + card.tipoCarta + " and color " + card.cardColor);
                            }
                        }
                        else
                        {
                            TirarCarta();
                            card.OnMouseDown();
                            //Imprimir que tipo de carta se tiro y su color
                            Debug.Log("Player " + currentPlayer.name + " threw a card of type " + card.tipoCarta + " and color " + card.cardColor);
                        }

                    }
                    //!currentPlayer.hasDrawnCard
                    if (currentPlayer.hasThrownCard || currentPlayer.hasPlayedCard)
                    {
                        DrawCard();
                    }
                    //!currentPlayer.hasRolledDice
                    if (!currentPlayer.hasRolledDice)
                    {
                        StartCoroutine(dice.RollTheDice());
                    }
                }

                yield return new WaitForSeconds(5.5f);
            }

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        // If the game is running in the Unity editor...
        #if UNITY_EDITOR
        // Disable VSync.
        QualitySettings.vSyncCount = 0;
        // Set the target frame rate to 45 FPS.
        Application.targetFrameRate = 45;
        #endif
    }


}
