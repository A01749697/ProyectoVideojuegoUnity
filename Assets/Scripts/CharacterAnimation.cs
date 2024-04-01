using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    //Referncia al animator
    private Animator animator;
    //Hacer referencia al script Path del personaje
    //referencia al sprite renderer
    public SpriteRenderer spriteRenderer;
    private Path path;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        path = GetComponent<Path>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!path.moveAllowed)
        {
            animator.SetBool("mov", false);
        }
        else
        {
            animator.SetBool("mov", true);
            if(transform.position.x < path.waypoints[path.waypointIndex].position.x){
                animator.SetBool("lados", true);
                spriteRenderer.flipX = true;
            }else if(transform.position.x > path.waypoints[path.waypointIndex].position.x){
                animator.SetBool("lados", true);
                spriteRenderer.flipX = false;
            }else if(transform.position.y < path.waypoints[path.waypointIndex].position.y){
                animator.SetBool("arriba", true);
            }else if(transform.position.y > path.waypoints[path.waypointIndex].position.y){
                animator.SetBool("abajo", true);
            }
            if((Vector2)transform.position == (Vector2)path.waypoints[path.waypointIndex].transform.position){
                animator.SetBool("arriba", false);
                animator.SetBool("abajo", false);
                animator.SetBool("lados", false);
                animator.SetBool("mov", false);
            }
        }
    }
}
