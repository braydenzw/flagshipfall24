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
*   - Some time based updating of currently slotted Pizzas (cook them)
*/

public class OvensStation : MonoBehaviour
{
    // TODO: some ordered container of pizza objects

    // TODO: add some timing var(s)

    // Start is called before the first frame update
    void Start()
    {
        // do any setup here
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: use this to update Pizza.cookLevel of all occupied slots on some interval
    }

    // TODO: something handling user interaction
    // TODO: something handling placement of pizza object (put down and pick up)
    // TODO: something handling "cooking" process
}
