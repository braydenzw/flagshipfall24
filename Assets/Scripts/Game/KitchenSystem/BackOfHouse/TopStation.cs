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
*   - Based on the performance in mini-game, Pizza object is updated with addTopping() function (actual name might be diff)
*/


public class TopStation : MonoBehaviour
{
    const int pizzaTopSlots = 4; // TODO: define number of slots here, can be whatever you think is appropraite (probably not more than like 10 though)
    private Pizza[] pizzas; // ordered list of pizza data objects

    // Start is called before the first frame update
    void Start()
    {
        // do any setup here
        pizzas = new Pizza[pizzaTopSlots];
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: something handling user interaction
    // TODO: something handling placement of pizza object (put down and pick up)
    // TODO: something handling actual mini-game
        // mini-game should reference some index in pizzas array to update Pizza class
}
