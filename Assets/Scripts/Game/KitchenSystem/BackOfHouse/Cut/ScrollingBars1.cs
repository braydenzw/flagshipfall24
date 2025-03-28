

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;

//using static System.Net.Mime.MediaTypeNames;

public class ScrollingBars : MonoBehaviour
{
    public GameObject barSprite; // Reference to the specific sprite for the bar
    public float barSpeed = 20f; // Speed of bar movement
    public float screenPadding = 0.5f; // Padding for the bar position

    private float screenWidth;
    public int sliceCount = 4;
    public int clickAttempts = 4;

    public Text scoreDisplay;
    public int score = 0;

    SpriteRenderer spriteSettings;

    void Start()
    {
        // Get screen width in world units
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        scoreDisplay.enabled = true;
        scoreDisplay.text = "Score: " + score;
        spriteSettings = GetComponent<SpriteRenderer>();
        //if (barSprite == null)
        //{
        //    Debug.LogError("Bar sprite is not assigned!");
        //}
    }

    int returnScore (int cutScore)
    {
        return cutScore;
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
            if (sliceCount == 1) {
                returnScore(score);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (clickAttempts > 0)
            {
                float distance = Math.Abs(barSprite.transform.position.x - (screenWidth / 2f)) / (screenWidth / 2f);
                score += ((int) (distance * 30));
                if (score > 100)
                {
                    score = 100;
                }
                
            }
            clickAttempts--;
            if(clickAttempts == 3)
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


