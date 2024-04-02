using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;
    
    [SerializeField]
    private float moveSpeed = 2f;

    [HideInInspector]
    public int waypointIndex = 0;
    public bool moveAllowed = false;
    private bool coroutineAllowed = true;

    void Start()
    {
        transform.position = new Vector2(5f,-0.59f);
    }

    void Update()
    {
        if(moveAllowed && coroutineAllowed){
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move(){
        coroutineAllowed = false;
        for(int i=0; i < GameManager.diceSideThrown; i++){
            if(waypointIndex == 23){waypointIndex = 0;}            
            while ((Vector2)transform.position != (Vector2)waypoints[waypointIndex].transform.position)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            waypointIndex++;
        }
        moveAllowed = false;
        coroutineAllowed = true;
    }
}
