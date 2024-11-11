using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This is an actual Unity script that should allow the player to interact with this station
*  The actual implementation of this is up to whoever is scripting this...
*  
*  Requirements:
*   - User can interact with the station somehow
*   - When interacting, user can begin some mini-game related to the station
*   - Based on the performance of the mini-game, some a new Pizza Unity object is created in the current scene
*   - Some signal is also sent to DayManager to keep track of this new object in the active data
*/

public class TossStation : MonoBehaviour
{

    public GameObject pizza; // actual prefab to be used when creating new Unity object
    public DayManager dayManager; // requires this to call data management function
    public KeyCode interactionKey = KeyCode.E;
    public enum GameStation
    {
        Game1 = 1,
        Game2 = 2,
        Game3 = 3,
        Game4 = 4
    }
    // Dictionary to store each station's highlight effect and prompt UI by their unique ID
    private Dictionary<int, (GameObject highlight, GameObject prompt)> stationElements = new Dictionary<int, (GameObject, GameObject)>();

    private int currentStationID = -1; // Tracks the current station ID the player is near




    // Start is called before the first frame update
    void Start()
    {
        // do any setup here
        dayManager = gameObject.AddComponent<DayManager>();
        stationElements.Add(1, (GameObject.Find("Highlight1"), GameObject.Find("PromptUI1")));
        stationElements.Add(2, (GameObject.Find("Highlight2"), GameObject.Find("PromptUI2")));
        stationElements.Add(3, (GameObject.Find("Highlight3"), GameObject.Find("PromptUI3")));
        // Disable highlight and prompt initially
        foreach (var element in stationElements.Values)
        {
            element.highlight.SetActive(false);
            element.prompt.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStationID != -1 && Input.GetKeyDown(interactionKey))
        {
            GameStation gameStation = (GameStation)currentStationID;
            EnterMinigame(gameStation);
        }
    }

    // TODO: something handling user interaction
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider has a StationID component
        // StationID station = other.GetComponent<StationID>();
        // if (station != null)
        // {
        //     currentStationID = station.id;

        //     // Activate the corresponding highlight and prompt for this station
        //     if (stationElements.ContainsKey(currentStationID))
        //     {
        //         var elements = stationElements[currentStationID];
        //         elements.highlight.SetActive(true);
        //         elements.prompt.SetActive(true);
        //     }
        // }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if exiting the same station the player entered
        // StationID station = other.GetComponent<StationID>();
        // if (station != null && station.id == currentStationID)
        // {
        //     // Disable the highlight and prompt for this station
        //     if (stationElements.ContainsKey(currentStationID))
        //     {
        //         var elements = stationElements[currentStationID];
        //         elements.highlight.SetActive(false);
        //         elements.prompt.SetActive(false);
        //     }

        //     currentStationID = -1; // Reset the station ID
        // }
    }
    // TODO: something handling actual mini-game
    void EnterMinigame(GameStation stationID)
    {
        Debug.Log("Entering minigame at station " + stationID);
        switch (stationID)
        {
            case GameStation.Game1:
                Debug.Log("Starting Game 1");
                break;
            case GameStation.Game2:
                Debug.Log("Starting Game 2");
                break;
            case GameStation.Game3:
                Debug.Log("Starting Game 3");
                break;
            case GameStation.Game4:
                Debug.Log("Starting Game 4");
                break;
            default:
                Debug.Log("Unknown Game");
                break;
        }
    }
    // TODO: some function to create new Pizza prefab
    void CreatePizza(Pizza pizzaPrefab)
    {
        // Instantiate the Pizza GameObject from a prefab
        // GameObject pizzaObject = Instantiate(pizzaPrefab);
        
        // // Position it in the center of the screen
        // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 10f);
        // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        
        // pizzaObject.transform.position = worldPosition;

        // // Add and configure Pizza component if necessary
        // pizza = pizzaObject.AddComponent<Pizza>();
        // pizza.UpdateCookLevel(0.5f); // Example usage
    }

}
