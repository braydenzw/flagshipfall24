using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
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
    private DayManager dayManager;
    private string mainScene = "kitchenScene";

    [Header("Game Vars")]
    public GameObject barSprite; 
    public float barSpeed = 20f; 
    public float screenPadding = 0.5f; 

    private float screenWidth;
    public int sliceCount = 4;
    public int clickAttempts = 4;
    private bool gameRunning = true;
    private int score = 0;

    [Header("UI stuff")]
    public Text scoreDisplay;
    public TMP_Text countdownTxt;
    public float countdown = 3f;

    [Header("Game End")]
    public GameObject endGame;
    public OrderUI real;
    public OrderUI expected;

    void Start()
    {
        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(false);
        dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();

        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        scoreDisplay.enabled = true;
        scoreDisplay.text = "Score: " + score;
    }
    
    void Update()
    {
        if (countdown > 0f) {
            countdownTxt.text = "" + Math.Ceiling(countdown);
            countdown -= Time.deltaTime;
        } else if(gameRunning) {
            countdownTxt.text = "";
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

                    if(gameRunning && sliceCount <= 0) {
                        barSprite = null;
                        gameRunning = false;

                        // show end of scene options to destroy pizza or submit it
                        var playerOrder = dayManager.getUserOrder();
                        var playerPizza = dayManager.getUserPizza();
                        expected.setOrder(playerOrder, 0);
                        var curr = new Order(playerOrder);
                        curr.expected = playerPizza;
                        real.setOrder(curr, 0);

                        endGame.SetActive(true);
                    }
                }
            }

            if (sliceCount > 0 && Input.GetKeyDown(KeyCode.Space)) //Click space when moving bar is over square. You have 4 tries
            {
                if (clickAttempts > 0)
                {
                    float distance = Math.Abs(barSprite.transform.position.x - (screenWidth / 2f)) / (screenWidth / 2f);
                    score += ((int)(distance * 30)); //I made the scoring generous :)
                    if (score > 100)
                    {
                        score = 100;
                    }
                    scoreDisplay.text = "Score: " + score;
                }

                clickAttempts--;

                if(gameRunning && clickAttempts <= 0) {
                    barSprite = null;
                    gameRunning = false;

                    // update cut value based on result
                    dayManager.setCutValue(DayManager.PLAYER_ID, score);

                    // show end of scene options to destroy pizza or submit it
                    var playerOrder = dayManager.getUserOrder();
                    var playerPizza = dayManager.getUserPizza();
                    expected.setOrder(playerOrder, 0);
                    var curr = new Order(playerOrder);
                    curr.expected = playerPizza;
                    real.setOrder(curr, 0);

                    endGame.SetActive(true);
                }

                // if (clickAttempts == 3)
                // {
                //     spriteSettings.color = new Color(1, 0, 0, 1);
                // }
                // if (clickAttempts == 2)
                // {
                //     spriteSettings.color = new Color(0, 1, 0, 1);
                // }
                // if (clickAttempts == 1)
                // {
                //     spriteSettings.color = new Color(0, 1, 1, 1);
                // }
                // if (clickAttempts == 0)
                // {
                //     spriteSettings.color = new Color(0, 0, 1, 1);
                // }
            }
        }
    }

    public void destroyPizza() {
        // clean up memory stored by dayManager
        dayManager.destroyPizza(DayManager.PLAYER_ID);

        GameObject.Find("Player").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.LoadScene(mainScene);
    }

    public void submitPizza() {
        // clean up dayManager, but submit order as well
        dayManager.submitOrder(DayManager.PLAYER_ID);

        GameObject.Find("Player").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.LoadScene(mainScene);
    }
}