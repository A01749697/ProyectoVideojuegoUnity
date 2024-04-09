using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class barraAnimationControl : MonoBehaviour
{
    private Animator animatorUI;
    private bool clicked = false;

    void Start()
    {
        animatorUI = GetComponent<Animator>();
    }

    public void eventoClick()
    {
        animatorUI.SetBool("clicked", !clicked);
        clicked = !clicked;
    }

}