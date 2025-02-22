using System.Collections.Generic;
using UnityEngine;

/*
* This script should manage a "day" in the game
*   This means handling all kitchen stuff
*   This should interact with (emit?) the GameManager to update the overall state of the game
*     (e.g. adding profit, orders completed, etc.)
*/

// TODO: if we add some pause system, this way of generating orders needs to change

public class DayManager : MonoBehaviour
{
    private static GameObject dayManager;

    // data handlers
    private StaffManager staffManager;
    private OrderManager orderManager;
    private Dictionary<int, Pizza> activePizzas;
    private Dictionary<Pizza, Order> pizzaToOrder;
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
        activePizzas = new Dictionary<int, Pizza>();
        pizzaToOrder = new Dictionary<Pizza, Order>();

        timer = 59f;
        playerLvl = GameManager.gameData.playerStats.attr.lvl;
        if(playerLvl == 0){
            playerLvl = 1;
        }
        orderInterval /= (playerLvl + 1f) / 2f; // TODO: decide on scaling
        DontDestroyOnLoad(this.gameObject); // TODO: remember to delete this after every game, but since we're working with different scenes it's necessary
        if (dayManager == null) {
		    dayManager = this.gameObject;
        } else {
            Destroy(this.gameObject);
        }
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
    public void assignPizzaToOrder(Pizza p, Order o){ pizzaToOrder.Add(p, o); }
    public void assignGameObjToPizza(int goID, Pizza p){ activePizzas.Add(goID, p); }
    public void swapGameObjPizza(int goExisting, int goNew){
        activePizzas.Add(goNew, activePizzas[goExisting]);
        activePizzas.Remove(goExisting);
    }
    public void submitPizza(int gameObjectID){
        Pizza p = activePizzas[gameObjectID];
        activePizzas.Remove(gameObjectID);
        pizzaToOrder[p].submit(p);
        pizzaToOrder.Remove(p);
    }

    // TODO: some function to handle order submissions (basically just send it to the order manager)
        // we can get feedback from ordermanager, or we could just not (updates will happen at end of "day")
    
    // TODO: way to end day -> destroy DNDestroy objects, load scene w/ final stuff
}
