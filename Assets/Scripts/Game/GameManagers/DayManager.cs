using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This script should manage a "day" in the game
*   This means handling all kitchen stuff
*   This should interact with (emit?) the GameManager to update the overall state of the game
*     (e.g. adding profit, orders completed, etc.)
*/

public class DayManager : MonoBehaviour
{
    private KitchenSystem kitchenSystem;

    // TODO: create a bunch of timing vars

    // Start is called before the first frame update
    void Start()
    {
        // initialize the KitchenSystem object (using save state to initialize staff manager obj)
        kitchenSystem = new KitchenSystem(GameManager.gameData.staffData);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: generate new orders on some interval
            // using kitchenSystem.
    }

    // TODO: some function to handle pizza object creation (in data)

    // TODO: some function to handle order submissions (basically just send it to the order manager)
        // we can get feedback from ordermanager, or we could just not (updates will happen at end of "day")
}
