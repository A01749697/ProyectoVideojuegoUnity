using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 = cultivo
// 1 = plaga
// 2= spray

// 0 = verde
// 1 = rojo
// 2 = azul
// 3 = amarillo

public class Cards : MonoBehaviour
{
    private static int currentHighestOrderInLayer = 0;
    public int handIndex;
    public bool cardOnHand;
    public int cardColor;
    public Player player;
    [SerializeField]
    private int tipoCarta;
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
        if (player.hasPlayedCard)
        {
            return;
        }

        //Si el tipo de carta es de tipo "crop", imprimir "El jugador jugo un cultivo"
        if (tipoCarta == 0)
        {
            Debug.Log("El jugador jugo un cultivo");
        }//Si el tipo de carta es tipo "plague", imprimir "El jugador jugo una plaga"
        else if (tipoCarta == 1)
        {
            Debug.Log("El jugador jugo una plaga");
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
    }

    private void Update()
    {
        if (GameManager.modoTirarCarta && cardOnHand && colorChangeCoroutine == null)
        {
            colorChangeCoroutine = StartCoroutine(ChangeColorOverTime());
        }
        else if ((!GameManager.modoTirarCarta || !cardOnHand) && colorChangeCoroutine != null)
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
