using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CultivoPanelController : MonoBehaviour
{
	public float displayTime = 2f; // Time in seconds to display the panel

    private float timer = 0f;
    private bool isDisplaying = false;

    public Text messageText; // Texto para el mensaje
   // public Image cultivoImage; // Imagen para el cultivo
    public Sprite[] cultivoSprites; // Lista de imágenes para cada cultivo

    public void ShowPanel(string characterName, int cultivoColor)
    {
        // Configurar el texto del mensaje
        messageText.text = $"{characterName} jugó un cultivo: {GetColorName(cultivoColor)}";

        // Configurar la imagen del cultivo
        //cultivoImage.sprite = cultivoSprites[cultivoColor];

        // Mostrar el panel
        gameObject.SetActive(true);
        timer = 0f;
        isDisplaying = true;
        // Iniciar coroutine para desactivar el panel después de 3 segundos
        //StartCoroutine(HidePanelAfterDelay(3));
    }

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Desactivar el panel
    }

    private string GetColorName(int colorCode)
    {
        switch (colorCode)
        {
            case 0:
                return "verde";
            case 1:
                return "rojo";
            case 2:
                return "azul";
            case 3:
                return "amarillo";
            default:
                return "desconocido";
        }
    }
    private void Update()
    {
        if (isDisplaying)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // If the timer exceeds the display time, hide the panel
            if (timer >= displayTime)
            {
                // Reset variables
                isDisplaying = false;
                gameObject.SetActive(false);
            }
        }
    }
}
