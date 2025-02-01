using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class handling keybinds and controls for the player character
*
* Contributors: Caleb Huerta-Henry
* Last Updated: 11/24/2024
*/

public class PlayerControls : MonoBehaviour
{
    public float speed = 3f;
    public KeyCode orderUIKey = KeyCode.E;

    private Rigidbody2D rb;

    // essentially local vars
    private float horizInput;
    private float vertInput;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gets horizontal and vertical input and moves the Player's Rigidbody component accordingly
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(horizInput * speed, vertInput * speed);

        // toggle active order UI
        if(Input.GetKeyDown(orderUIKey)){
            var ui = GameObject.Find("UI");
            if(ui != null){
                var orderUI = ui.GetComponent<Transform>().GetChild(0).gameObject;
                orderUI.SetActive(!orderUI.activeSelf);
            } else {
                Debug.LogWarning("Could not find OrderUI object");
            }
        }
    }
}
