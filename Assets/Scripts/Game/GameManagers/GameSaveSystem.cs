using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/*
* This is a static class that should be used to manage the save file of the game
*  It should be able to load a save file from a particular file path
*  And write to that loaded file when the game is 'saved'
*/

// TODO: decide if we care about encrypting our save data???
// realistically it probably shouldn't be a priority to stop cheating
public static class GameSaveSystem {
    private static GameData gameData;
    private static Dictionary<SaveFile, string> saves;
    private static string saveFileDirectory = Path.Join(Application.persistentDataPath, "Saves");
    private static string currentSaveFile = null;

    public static Dictionary<SaveFile, string> loadAllSaves(){
        // partial load of relevant data to saves
        if(saves == null) {
            saves = new Dictionary<SaveFile, string>();
            foreach(string file in Directory.GetFiles(saveFileDirectory)){
                if(file.EndsWith(".json")){
                    string contents = File.ReadAllText(file);
                    if(contents != ""){
                        var s = JsonUtility.FromJson<SaveFile>(contents);
                        saves[s] = file;
                    }
                }
            }
        }
        return saves;
    }

    public static GameData loadSave(string saveFile){
        if(File.Exists(saveFile)) {
            string json = File.ReadAllText(saveFile);
            currentSaveFile = saveFile;
            PlayerPrefs.SetString("LastSave", saveFile);
            return JsonUtility.FromJson<GameData>(json);
        } else {
            Debug.LogError("Requested save file not found: " + saveFile);
            return null;
        }
    }

    public static GameData createNewSave(string name){
        gameData = new GameData(name, 1, new PlayerStats(), new StaffData());
        if(!Directory.Exists(saveFileDirectory)){
            Directory.CreateDirectory(saveFileDirectory);
        }

        string newSave = Path.Join(saveFileDirectory, name + ".json");
        while(Directory.GetFiles(saveFileDirectory).Contains(newSave)){
            // don't let save files have same name, or we'll overwrite data
            newSave = Path.Join(saveFileDirectory, name + "_1.json");
        }
        currentSaveFile = newSave;
        PlayerPrefs.SetString("LastSave", newSave);
        File.WriteAllText(newSave, JsonUtility.ToJson(gameData, true));
        saves[new SaveFile(name, 1)] = newSave;
        return gameData;
    }

    public static bool saveGame(GameData newData){
        if(currentSaveFile == null){
            Debug.LogError("Attempted to save game without active save file.");
            return false;
        } else {
            File.WriteAllText(currentSaveFile, JsonUtility.ToJson(newData, true));
            Debug.Log("Game Saved to: " + currentSaveFile);
            return true;
        }
    }
}
