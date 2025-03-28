using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System;
//using static System.Net.Mime.MediaTypeNames;

/*
* This is an actual Unity script that should allow the player to interact with this station
*  The actual implementation of this is up to whoever is scripting this...
*  
*  Requirements:
*   - User can interact with the station somehow
*   - When interacting, user can:
*       - Place a pizza down in an unoccupied slot
*       - Pick up a pizza from an occupied slot
*       - Begin some mini-game related to the station
*           - Based on the performance in mini-game, Pizza object is updated
*       - Send out/submit a pizza object
*/



public class CutStation : MonoBehaviour
{
    const int pizzaCutSlots = 10; // TODO: define number of slots here, can be whatever you think is appropriate
    private Pizza[] pizzas; // ordered list of pizza data objects
    public DayManager dayManager; // needed for order submissions


    public GameObject barSprite; 
    public float barSpeed = 20f; 
    public float screenPadding = 0.5f; 

    private float screenWidth;
    public int sliceCount = 4;
    public int clickAttempts = 4;

    public Text scoreDisplay;
    public int score = 0;

    SpriteRenderer spriteSettings;


    void Start()
    {
        
        pizzas = new Pizza[pizzaCutSlots];
        

        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        scoreDisplay.enabled = true;
        scoreDisplay.text = "Score: " + score;
        spriteSettings = GetComponent<SpriteRenderer>(); //I still can't get text to work, but this would display the score

    }

    
    void Update()
    {
        if (barSprite != null)
        {
            // Move the bar to the right
            barSprite.transform.Translate(Vector3.right * barSpeed * Time.deltaTime);

            // Wrap around when the bar moves off the right side
            if ((barSprite.transform.position.x > screenWidth / 2f + screenPadding) && sliceCount > 1)
            {
                barSprite.transform.position = new Vector3(-screenWidth / 2f - screenPadding,
                    barSprite.transform.position.y,
                    barSprite.transform.position.z);
                barSpeed = barSpeed + 12f;
                sliceCount--;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space)) //Click space when moving bar is over square. You have 4 tries
        {

            if (clickAttempts > 0)
            {
                float distance = Math.Abs(barSprite.transform.position.x - (screenWidth / 2f)) / (screenWidth / 2f);
                score += ((int)(distance * 30)); //I made the scoring generous :)
                if (score > 100)
                {
                    score = 100;
                }

            }
            clickAttempts--;
            if (clickAttempts == 3)
            {
                spriteSettings.color = new Color(1, 0, 0, 1);
            }
            if (clickAttempts == 2)
            {
                spriteSettings.color = new Color(0, 1, 0, 1);
            }
            if (clickAttempts == 1)
            {
                spriteSettings.color = new Color(0, 1, 1, 1);
            }
            if (clickAttempts == 0)
            {
                spriteSettings.color = new Color(0, 0, 1, 1);
            }
        }
        scoreDisplay.text = "Score: " + score;
    }
}
    // TODO: something handling user interaction
    // TODO: something handling placement of pizza object (put down and pick up)
    // TODO: something handling actual mini-game
    // mini-game should reference some index in pizzas array to update Pizza class
    // TODO: something handling submission of pizza object (should reference DayManager function)




