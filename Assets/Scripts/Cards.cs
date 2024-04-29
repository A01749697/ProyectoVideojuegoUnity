using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 0 = cultivo
// 1 = plaga
// 2= spray

// 0 = verde //calabaza
// 1 = rojo //frijol
// 2 = azul //zanahoria
// 3 = amarillo //trigo
public class Cards : MonoBehaviour
{
    private static int currentHighestOrderInLayer = 0;
    public int handIndex;
    public bool cardOnHand;
    public int cardColor;
    public Player player;
    [SerializeField]
    private int tipoCarta;
    public int tipoCultivo;
    private SpriteRenderer spriteRenderer;
    private Coroutine colorChangeCoroutine;
    private AudioSource AudioSource;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
        if (AudioSource == null){
            AudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get the name of the GameObject
        string cardName = gameObject.name;

        // Split the name by the '-' character
        string[] cardParts = cardName.Split('-');

        // Check the first part of the split name and assign the corresponding value to tipoCarta
        switch (cardParts[0])
        {
            case "cultivo":
                tipoCarta = 0;
                break;
            case "plaga":
                tipoCarta = 1;
                break;
            case "spray":
                tipoCarta = 2;
                break;
        }

        // Check the second part of the split name and assign the corresponding value to cardColor
        switch (cardParts[1])
        {
            case "verde":
                cardColor = 0;
                break;
            case "rojo":
                cardColor = 1;
                break;
            case "azul":
                cardColor = 2;
                break;
            case "amarillo":
                cardColor = 3;
                break;
        }
    }

    public void OnMouseDown()
    {
        //si el modo tirar carta esta activo, y se clikea una carta, la 
        //carta ya no estara en la mano del juagador, y se podra jugar fuera de la camara
        if (GameManager.instance.modoTirarCarta)
        {
            player.hasThrownCard = true;
            player.availableCardSlots[handIndex] = true;
            transform.position = new Vector3(12f, -1.75f, 0);
            cardOnHand = false;
            GameManager.instance.modoTirarCarta = false;
            return;
        }
        if (player.hasPlayedCard || player.hasThrownCard)
        {
            return;
        }
        //Si el jugador juega una carta de cultivo(que ya ha jugado anteriormente y sigue en el campo), es invalido
        if (tipoCarta == 0)
        {
            //Hacer un switch sobre el color de la carta
            switch (cardColor)
            {
                case 0:
                    if (player.cultivosAvailable[0])
                    {
                        return;
                    }
                    break;
                case 1:
                    if (player.cultivosAvailable[1])
                    {
                        return;
                    }
                    break;
                case 2:
                    if (player.cultivosAvailable[2])
                    {
                        return;
                    }
                    break;
                case 3:
                    if (player.cultivosAvailable[3])
                    {
                        return;
                    }
                    break;
            }
        }
        
        if (tipoCarta == 0){
            player.numberCultivos++;
            //Hacer un siwtch sobre el color de la carta
            switch (cardColor)
            {
                case 0:
                    player.cultivosAvailable[0] = true;
                    //Console Log
                    Debug.Log("El jugador ya jugo una carta de cultivo verde");
                    break;
                case 1:
                    //Console Log
                    Debug.Log("El jugador ya jugo una carta de cultivo rojo");
                    player.cultivosAvailable[1] = true;
                    break;
                case 2:
                    //Console Log
                    Debug.Log("El jugador ya jugo una carta de cultivo azul");
                    player.cultivosAvailable[2] = true;
                    break;
                case 3:
                    //Console Log
                    Debug.Log("El jugador ya jugo una carta de cultivo amarillo");
                    player.cultivosAvailable[3] = true;
                    break;
            }
            //Si el jugador 1 juega una carta
            if(GameManager.instance.currentPlayerIndex == 0){
                switch (cardColor)
                {
                    case 0:
                        //Activar el primer GameObject cultivos de GameManeger
                        GameManager.instance.cultivos[0].SetActive(true);
                        break;
                    case 1:
                        //Activar el segundo GameObject cultivos de GameManeger
                        GameManager.instance.cultivos[1].SetActive(true);
                        break;
                    case 2:
                        //Activar el tercer GameObject cultivos de GameManeger
                        GameManager.instance.cultivos[2].SetActive(true);
                        break;
                    case 3:
                        //Activar el cuarto GameObject cultivos de GameManeger
                        GameManager.instance.cultivos[3].SetActive(true);
                        break;
                }
            }
            HudGame.instance.UpdatePlayerCultivosUI(GameManager.instance.currentPlayerIndex+1, player.numberCultivos); 
        }else if (tipoCarta == 1){
            Debug.Log("El jugador jugo una plaga");

        }else if (tipoCarta == 2){
            Debug.Log("El jugador jugo un spray");
        }

        if(AudioSource != null && AudioSource.clip != null){
            AudioSource.Play();
        }
        
        player.availableCardSlots[handIndex] = true;
        player.hasPlayedCard = true;
        transform.position = new Vector3(7.6f, -1.75f, 0);
        //quitarle la rotacion a la carta
        transform.rotation = Quaternion.identity;
        cardOnHand = false;

        // Increment the current highest order in layer and set the card's order in layer to this value
        currentHighestOrderInLayer++;
        spriteRenderer.sortingOrder = currentHighestOrderInLayer;
        if(player.numberCultivos == 4){
            //game over
            Debug.Log("Game Over");
            GameManager.instance.gameOver = true;
            //Se activa el panel de fin de juego
            endGame.instance.EndGameMoment();
        }
    }

    private void Update()
    {
        if (GameManager.instance.modoTirarCarta && cardOnHand && colorChangeCoroutine == null)
        {
            colorChangeCoroutine = StartCoroutine(ChangeColorOverTime());
        }
        else if ((!GameManager.instance.modoTirarCarta || !cardOnHand) && colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
            colorChangeCoroutine = null;
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator ChangeColorOverTime(){
        Color startColor = Color.white;
        Color endColor = Color.red;
        float duration = 1f; // Change this to change the speed of the color change
        while (true){
            // Change from white to red
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(startColor, endColor, t / duration);
                yield return null;
            }

            // Change from red to white
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(endColor, startColor, t / duration);
                yield return null;
            }
        }
    }

}
