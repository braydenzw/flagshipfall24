using System;

/*
* This is a static class that should be used to manage the save file of the game
*  It should be able to load a save file from a particular file path
*  And write to that loaded file when the game is 'saved'
*/


// TODO: define save file system in project
 // probably look into Application.persistentDataPath

// TODO: decide if we care about encrypting our save data???
 // realistically it probably shouldn't be a priority to stop cheating
public static class GameSaveSystem {
    private static GameData gameData;
    private static string currentSaveFile;


    // set the actual save file and read the contents into gameData
    public static bool loadSaveFile(string file){
        currentSaveFile = file;

        // TODO: read the contents of the save file at this path
         // and de-serialize into a GameData object

        return true; // should return based on if the "load" was successful
    }

    // just give whoever needs access access to this (may be null)
    public static GameData loadGameData(){
        return gameData;
    }

    // TODO: function that writes a save file to memory
        // should serialize the current object and overwrite the save 
        // should return whether this action was completed successfully (handle IOExceptions, etc)
    public static bool saveGameData(GameData newGameData){
        return false;
    }
}
