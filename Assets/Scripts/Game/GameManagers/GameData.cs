/*
* This is a Serializable class that should contain all data that is relevant to a current save state
*
* Right now, this is just:
*  1. A PlayerStats object
*  2. A StaffData object
*
* Contributors: Caleb Huerta-Henry
* Last Updated: 10/5/2024
*/

using System;

[Serializable]
public class GameData {
    public string name;
    public int day;
    public PlayerStats playerStats;
    public StaffData staffData;

    public GameData(string name, int day, PlayerStats ps, StaffData sd){
        this.name = name;
        this.day = day;
        playerStats = ps;
        staffData = sd;
    }
}

// partial save state just so we can show the existing save files
// without having to load the entire thing
[Serializable]
public class SaveFile {
    public string name;
    public int day;
    public SaveFile(string name, int day){
        this.name = name;
        this.day = day;
    }
}