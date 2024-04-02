using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public int handIndex;
    public bool cardOnHand;
    public Player player;
    
    private void OnMouseDown()
    {
        player.availableCardSlots[handIndex] = true;
        transform.position = new Vector3(7.6f, -1.75f, 0);
        cardOnHand = false;
    }
}
