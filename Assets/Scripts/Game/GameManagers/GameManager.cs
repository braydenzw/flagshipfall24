using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This is the main game manager for whenever the game is actually running
*  When a save file is loaded, some static instance of this class will be generated (DontDestroyOnLoad)
*  This class will use the loaded save file on initialization and handle the start/end of "days"
*  This means updating the save files at the start of days, handling scene changes, etc.
* 
* If a game state is "ended", then this object should be destroyed
*  basically just when returning to main menu
*/

public class GameManager : MonoBehaviour
{
    private static GameObject gameManager;
    public static GameData gameData; // this should be the current state of the game
    // can be updated by other scripts (e.g. DayProgession should update this when the day ends)
    public string kitchenScene = "TestEnvironment";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (gameManager == null) {
		    gameManager = this.gameObject;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void beginDay(){
        // basically just load kitchen scene
        SceneManager.LoadScene(kitchenScene);
    }
    
    public void endDay(){
        // basically just save game
        GameSaveSystem.saveGame(gameData);

        // destroy static objs from kitchen?
        // and then go to some recap scene
    }
}