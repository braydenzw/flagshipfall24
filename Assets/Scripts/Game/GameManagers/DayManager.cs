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
    // data handlers
    private StaffManager staffManager;
    private OrderManager orderManager;
    private Kitchen kitchen;
    private int playerLvl;

    // TODO: create a bunch of timing vars
    public float orderInterval = 60f; // base time at level 1
    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        if(GameManager.gameData != null && GameManager.gameData.staffData != null) {
            staffManager = new StaffManager(GameManager.gameData.staffData);
        } else {
            staffManager = new StaffManager(new StaffData());
        }
        orderManager = gameObject.AddComponent<OrderManager>();
        kitchen = new Kitchen();

        timer = 59f;
        playerLvl = GameManager.gameData.playerStats.attr.lvl;
        if(playerLvl == 0){
            playerLvl = 1;
        }
        orderInterval /= (playerLvl + 1f) / 2f; // TODO: decide on scaling
        DontDestroyOnLoad(this); // TODO: remember to delete this after every game, but since we're working with different scenes it's necessary
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: if we add some pause system, this needs to change
        // generate new orders on some interval
        if(timer >= orderInterval){
            // TODO: should link to some real customer name or object?
            orderManager.generateOrder(GameManager.gameData.playerStats, "Test Name");
            timer = 0f;
        } else {
            timer += Time.deltaTime;
        }
    }

    // TODO: some function to handle pizza object creation (in data)

    // TODO: some function to handle order submissions (basically just send it to the order manager)
        // we can get feedback from ordermanager, or we could just not (updates will happen at end of "day")
}
