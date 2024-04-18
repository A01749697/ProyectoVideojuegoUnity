using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0 = crops
// 1 = plague
public class Cards : MonoBehaviour
{
    public int handIndex;
    public bool cardOnHand;
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
    }

    public void OnMouseDown()
    {

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
        transform.position = new Vector3(7.6f, -1.75f, 0);
        cardOnHand = false;
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
            spriteRenderer.color = Color.white; // Reset to white when stopping
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
