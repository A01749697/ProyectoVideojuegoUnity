using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;
    public AudioClip footstepsSound; // Variable para almacenar el audio de pisadas
    private AudioSource audioSource; // Variable para el AudioSource

    [SerializeField]
    private float moveSpeed = 2f;

    [HideInInspector]
    public int waypointIndex = 0;
    public bool moveAllowed = false;
    private bool coroutineAllowed = true;

    public void Start()
    {
        transform.position = new Vector2(5f,-0.59f);
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource del personaje
    }

    public void Update()
    {
        if(moveAllowed && coroutineAllowed){
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move(){
        coroutineAllowed = false;
        for(int i=0; i < GameManager.instance.diceSideThrown; i++){
            if(waypointIndex == 24){
                //Cada vez que un jugador llega al final, se le añade un cultivo de forma automatica
                GameManager.instance.currentPlayer.numberCultivos++;  
                //Recorrero las availableCultivos del jugador Actual para encontrar un cultivo disponible, si lo hay, se vuelve true
                for(int j = 0; j < 4; j++){
                    if(!GameManager.instance.currentPlayer.cultivosAvailable[j]){
                        GameManager.instance.currentPlayer.cultivosAvailable[j] = true;
                        //Activar cultivo correspondiente del gameManager solo si es el jugador 1
                        if(GameManager.instance.currentPlayerIndex == 0){
                            GameManager.instance.cultivos[j].SetActive(true);
                        }
                        //Actualizar el Hud de los cultivos
                        HudGame.instance.UpdatePlayerCultivosUI(GameManager.instance.currentPlayerIndex+1,GameManager.instance.currentPlayer.numberCultivos); 
                        //Si el jugador actual tiene 4 cultivos: Game Over
                        if(GameManager.instance.currentPlayer.numberCultivos == 4){
                            GameManager.instance.gameOver = true;
                            endGame.instance.EndGameMoment();
                        }
                        break;
                    }
                }

                waypointIndex = 0;
                HudGame.instance.UpdatePlayerInformationUI(Dice.playerIndex, waypointIndex);
            }            
            while ((Vector2)transform.position != (Vector2)waypoints[waypointIndex].transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                // Reproducir el sonido de pisadas mientras el personaje se mueve
                if (!audioSource.isPlaying && footstepsSound != null)
                {
                    audioSource.clip = footstepsSound;
                    audioSource.Play();
                }
                yield return null;
            }
            waypointIndex++;
            HudGame.instance.UpdatePlayerInformationUI(GameManager.instance.currentPlayerIndex+1, waypointIndex);
        }
        moveAllowed = false;
        coroutineAllowed = true;
        GameManager.instance.currentPlayer.hasRolledDice = true;
    }
}

