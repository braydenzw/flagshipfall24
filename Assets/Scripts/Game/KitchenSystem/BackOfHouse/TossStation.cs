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
*     - Some signal is also sent to DayManager to keep track of this new object in the active data
*/

public class TossStation : MonoBehaviour
{
    public GameObject pizza; // actual prefab to be used when creating new Unity object
    public DayManager dayManager; // requires this to call data management function

    // Start is called before the first frame update
    void Start()
    {
        // do any setup here
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: something handling user interaction
    // TODO: something handling actual mini-game
    // TODO: some function to create new Pizza prefab
}
