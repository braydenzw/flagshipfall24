using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script should manage a "day" in the game
*   This means handling all kitchen stuff
*   This should interact with (emit?) the GameManager to update the overall state of the game
*     (e.g. adding profit, orders completed, etc.)
*/

// TODO: if we add some pause system, this way of generating orders needs to change
// TODO: need to figure out order names (can just be a list of like 100 strings)

public class DayManager : MonoBehaviour
{
    public static string PLAYER_ID = "PLAYER";

    [Header("Scenes")]
    public string mainScene = "KitchenScene";
    public string endOfDayScene = "";
    private static GameObject dayManager;

    // data handlers
    private StaffManager staffManager;
    private OrderManager orderManager;
    private Dictionary<string, Pizza> activePizzas;
    private Dictionary<Pizza, Order> pizzaToOrder;
    private int playerLvl;

    [Header("Timing")]
    // TODO: decide how long a day should be, 5min rn
    public float dayStart = 60f * 12; // in terms of AM-PM stuff // start at noon for now i guess?
    public float dayEnd = 60f * (12 + 10); // in terms of AM-PM stuff // for now 10pm
    private float dayInterval; // time between end and start
    public float dayTimerDuration = 60f * 6; // actual time a day takes in seconds
    private float dayTimer; // current timer in seconds

    public float orderInterval = 60f; // base time at level 1
    private float orderTimer;

    [Header("UI")]
    private TMP_Text dayTimeText;
    private TMP_Text orderProfitText;
    private GameObject endDayButton;

    // Start is called before the first frame update
    private void Start()
    {
        if(GameManager.gameData != null && GameManager.gameData.staffData != null) {
            staffManager = new StaffManager(GameManager.gameData.staffData);
        } else {
            Debug.LogError("GameData did not have staff data...");
            staffManager = new StaffManager(new StaffData());
        }
        orderManager = gameObject.AddComponent<OrderManager>();
        activePizzas = new Dictionary<string, Pizza>();
        pizzaToOrder = new Dictionary<Pizza, Order>();
        playerLvl = Math.Max(1, GameManager.gameData.playerStats.attr.lvl);

        orderInterval /= (playerLvl + 1f) / 2f; // TODO: decide on scaling
        orderTimer = 0.95f * orderInterval;
        Debug.Log(orderInterval);
        dayTimer = 0f;
        dayInterval = dayEnd - dayStart;

        DontDestroyOnLoad(this.gameObject);
        if (dayManager == null) {
		    dayManager = this.gameObject;
        } else {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void Update() {
        if(dayTimer < dayTimerDuration){
            // only generate new orders if they're guaranteed to be due before close
            if(dayTimerDuration - orderManager.getMaxOrderTime() > dayTimer){
                if(orderTimer >= orderInterval){
                    // TODO: should link to some real customer name or object?
                    Debug.Log("New order!");
                    orderManager.generateOrder(GameManager.gameData.playerStats, "Test Name");
                    orderTimer = 0f;
                } else {
                    orderTimer += Time.deltaTime;
                }
            }

            dayTimer += Time.deltaTime;
            if(SceneManager.GetActiveScene().name == mainScene) {
                updateDayTimeUI();
                updateOrderUI();
            }
        } else if((endDayButton == null || endDayButton.activeSelf) && orderManager.numOrders() == orderManager.numCompletedOrders()){
            // if all orders completed and timer completed, allow end of day
            if(endDayButton == null){
                endDayButton = GameObject.Find("endDayButton");
            }
            endDayButton.SetActive(true);
        }
    }

    private void updateDayTimeUI() {
        if(dayTimeText == null){
            dayTimeText = GameObject.Find("dayTimeText").GetComponent<TMP_Text>();
        }
 
        if(dayTimer < dayTimerDuration){
            float gameTime = dayStart + dayInterval * dayTimer / dayTimerDuration;
            int hours = (int)(gameTime / 60);
            int minutes = (int)(gameTime % 60);
            dayTimeText.text = (hours > 12 ? (hours - 12).ToString() : hours) + (hours < 12 ? "AM" : "PM");
            // dayTimeText.text = (hours > 12 ? (hours - 12).ToString() : hours) + ":" + minutes.ToString("00") + (hours > 12 ? "AM" : "PM");
        } else {
            dayTimeText.text = "CLOSED";
        }
    }

    private void updateOrderUI() {
        if(orderProfitText == null) {
            orderProfitText = GameObject.Find("orderProfitText").GetComponent<TMP_Text>();
        }

        var om = dayManager.GetComponent<OrderManager>();
        orderProfitText.text = "Orders: " + om.numCompletedOrders() + "/" + (om.numOrders() + om.numCompletedOrders()) + "\nProfit: " + Math.Round(om.getDayProfit(), 2);
    }


    public void printActivePizzas() {
        foreach(Pizza p in activePizzas.Values){
            Debug.Log("ACTIVE PIZZA: " + p);
        }
    }
    public bool pizzaExistsWithID(string id) { return activePizzas != null && activePizzas.ContainsKey(id); }
    public Order getUserOrder() { return getOrderFromID(PLAYER_ID); }
    public Pizza getUserPizza() { return activePizzas[PLAYER_ID]; }
    public void destroyPizza(string key) {
        pizzaToOrder.Remove(activePizzas[key]);
        activePizzas.Remove(key);
        Debug.Log("Pizza from key " + key + " DESTROYED");
    }
    public Order getOrderFromID(string id) { return pizzaToOrder[activePizzas[id]]; }
    public void addToppings(string pizzaID, List<Topping> toppings) { activePizzas[pizzaID].toppings = toppings; }
    public void setCutValue(string pizzaID, int cutValue) { activePizzas[pizzaID].cutQuality = cutValue; }
    /// <summary>
    /// Link a Pizza p to some Order o. DayManager will remember this assignment.
    /// </summary>
    public void assignPizzaToOrder(Pizza p, Order o){ pizzaToOrder.Add(p, o); }
    /// <summary>
    /// Link a GameObject (represented by its GameObject.getInstanceID()) to some Pizza p. DayManager will remember this assignment.
    /// </summary>
    public void assignGameObjToPizza(string id, Pizza p){
        activePizzas.Add(id, p);
    }
    /// <summary>
    /// Whatever Pizza the key goExisting (GameObjectID) is linked to, unassign this and assign it to goNew instead.
    /// </summary>
    public void swapGameObjPizza(string existing, string replace){
        activePizzas.Add(replace, activePizzas[existing]);
        activePizzas.Remove(existing);
    }

    /// <summary>
    /// Takes in a pizza GameObject ID linked to a Pizza in the DayManager dictionary. Submits this Pizza to the order it's linked to.
    /// Interacts with internal OrderManager and UI to evaluate and record the quality of this submission.
    /// </summary>
    public void submitOrder(string key){
        if(!activePizzas.ContainsKey(key)){
            Debug.LogWarning("GameObject not linked to pizza in dictionary");
            return;
        }
        Pizza p = activePizzas[key];
        if(!pizzaToOrder.ContainsKey(p)){
            Debug.LogError("Pizza linked to GameObject not linked to Order");
            return;
        }
        Order o = pizzaToOrder[p];
        orderManager.submitOrder(o.getID(), p);
        Debug.Log("Pizza with key " + key + " submitted to order with ID " + o.getID());

        // clean up data
        destroyPizza(key);
    }


    // end day -> destroy DNDestroy objects, load scene w/ final stats
    public void endDay() {
        // Destroy player object
        var player = GameObject.Find("Player");
        if(player != null){
            Destroy(player);
        } else {
            Debug.LogError("DayManager.endDay() could not find player object");
        }

        // load end of day scene
        SceneManager.LoadScene(endOfDayScene);
    }
}
