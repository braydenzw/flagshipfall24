using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This class facilitates player movement leveraging Unity's provided InputManager
*
* Contributors: Caleb Huerta-Henry
* Last Updated: 10/3/2024
*/

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
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
    }
}
