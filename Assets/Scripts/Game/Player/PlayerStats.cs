using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This class should maintain all player statistics and should persist across scenes.
* There should be one static instance of this class created when a save file is loaded.
*  (In other words, it should initially load from some save state)
* 
* Statistics can be split up into two main categories:
*  1. Gameplay statistics (things like days played, money, xp level, etc)
*  2. Character attributes (this should follow the same protocol as the AI worker attributes)
*/

[Serializable]
public class PlayerStats {
    public CharacterAttributes attr;

    // TODO: create and manage game stats (profit, days played, etc)

    public PlayerStats() {
        // TODO: implement instantiation function
        attr = new CharacterAttributes();
    }

    // TODO: create functions that can be used to update stored data
}
