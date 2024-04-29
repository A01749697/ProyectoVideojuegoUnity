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
        for(int i=0; i < GameManager.diceSideThrown; i++){
            if(waypointIndex == 23){
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
            HudGame.instance.UpdatePlayerInformationUI(Dice.playerIndex, waypointIndex);
        }
        moveAllowed = false;
        coroutineAllowed = true;
        GameManager.currentPlayer.hasRolledDice = true;
    }
}

