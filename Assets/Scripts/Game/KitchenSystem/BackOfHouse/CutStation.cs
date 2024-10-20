using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This is an actual Unity script that should allow the player to interact with this station
*  The actual implementation of this is up to whoever is scripting this...
*  
*  Requirements:
*   - User can interact with the station somehow
*   - When interacting, user can:
*       - Place a pizza down in an unoccupied slot
*       - Pick up a pizza from an occupied slot
*       - Begin some mini-game related to the station
*           - Based on the performance in mini-game, Pizza object is updated
*       - Send out/submit a pizza object
*/


public class CutStation : MonoBehaviour
{
    const int pizzaCutSlots = 10; // TODO: define number of slots here, can be whatever you think is appropriate
    private Pizza[] pizzas; // ordered list of pizza data objects

    public DayManager dayManager; // needed for order submissions

    // Start is called before the first frame update
    void Start()
    {
        // do any setup here
        pizzas = new Pizza[pizzaCutSlots];
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: something handling user interaction
    // TODO: something handling placement of pizza object (put down and pick up)
    // TODO: something handling actual mini-game
        // mini-game should reference some index in pizzas array to update Pizza class
    // TODO: something handling submission of pizza object (should reference DayManager function)
}
