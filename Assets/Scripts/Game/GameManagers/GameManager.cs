using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static GameData gameData; // this should be the current state of the game
    // can be updated by other scripts (e.g. DayProgession should update this when the day ends)

    void Awake()
    {
        gameData = GameSaveSystem.loadGameData(); // load current game state
    }

    // TODO: some function to "begin" a day
     // load actual game scene basically (actual game scene should handle everything else)

    // TODO: some function to "end" current object -> save current state and destroy instance
}