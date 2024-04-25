using UnityEngine;
using UnityEngine.UI;

public class TurnAnnouncement : MonoBehaviour
{
    public Text turnText; // Reference to the Text component displaying the turn message
    public float displayTime = 2f; // Time in seconds to display the panel

    private float timer = 0f;
    private bool isDisplaying = false;

    // display the panel with the specified character's name
    public void DisplayTurn(string characterName)
    {
        // Set the text to display the character's name
        turnText.text = "Es el turno del " + characterName;

        gameObject.SetActive(true);

        timer = 0f;
        isDisplaying = true;
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
