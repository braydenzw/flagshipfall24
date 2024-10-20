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
    public PlayerStats playerStats;
    public StaffData staffData;

    public GameData(PlayerStats ps, StaffData sd){
        playerStats = ps;
        staffData = sd;
    }
}