using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Dice : MonoBehaviour
{
    static public int playerIndex=0;
    private Sprite[] ladoDado;
    private Sprite[] animDado;
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSource; //


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ladoDado = new Sprite[6];
        animDado = new Sprite[6];
        audioSource = GetComponent<AudioSource>(); //
        for (int i = 0; i < 6; i++){
            string path = $"Assets/Sprites/Dados/dado{i + 1}.png";
            string path2 = $"Assets/Sprites/Dados/anim{i + 1}.png";
            Debug.Log("Cargando sprite desde: " + path);
            ladoDado[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            animDado[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path2);
            if (ladoDado[i] == null) Debug.LogError($"No se pudo cargar el sprite desde: {path}");
        }
    }

    private void OnMouseDown()
    {
        if(!GameManager.instance.gameOver && GameManager.instance.currentPlayerIndex == playerIndex) StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        audioSource.Play(); //
        for(int i = 0; i <= 10; i++){
            randomDiceSide = Random.Range(0, 6);
            spriteRenderer.sprite = animDado[randomDiceSide];
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = ladoDado[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        GameManager.instance.diceSideThrown=randomDiceSide+1;
        GameManager.instance.currentPlayer.GetComponent<Path>().moveAllowed = true;
        playerIndex = (playerIndex + 1) % 4;
    }

}
