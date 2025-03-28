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
    [Header("Scenes")]
    public string mainScene = "TestEnviroment";
    public string endOfDayScene = "";
    private static GameObject dayManager;

    // data handlers
    private StaffManager staffManager;
    private OrderManager orderManager;
    private Dictionary<int, Pizza> activePizzas;
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
    private GameObject endDayButton;

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

        orderTimer = 59f;
        orderInterval /= (playerLvl + 1f) / 2f; // TODO: decide on scaling
        dayTimer = 0f;
        dayInterval = dayEnd - dayStart;

        playerLvl = GameManager.gameData.playerStats.attr.lvl;
        if(playerLvl == 0){ playerLvl = 1; }

        DontDestroyOnLoad(this.gameObject);
        if (dayManager == null) {
		    dayManager = this.gameObject;
        } else {
            // orders num and profit can only change in mini-game, so just have to check on scene load
            var orderProfitText = GameObject.Find("orderProfitText").GetComponent<TMP_Text>();
            if(orderProfitText != null){
                var om = dayManager.GetComponent<OrderManager>();
                orderProfitText.text = "Orders: " + om.numCompletedOrders() + "\nProfit: " + om.getDayProfit();
            }
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(dayTimer < dayTimerDuration){
            // only generate new orders if they're guaranteed to be due before close
            if(dayTimerDuration - orderManager.getMaxOrderTime() > dayTimer){
                if(orderTimer >= orderInterval){
                    // TODO: should link to some real customer name or object?
                    orderManager.generateOrder(GameManager.gameData.playerStats, "Test Name");
                    orderTimer = 0f;
                } else {
                    orderTimer += Time.deltaTime;
                }
            }

            dayTimer += Time.deltaTime;
            if(SceneManager.GetActiveScene().name == mainScene) {
                updateDayTimeUI();
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
            dayTimeText.text = (hours > 12 ? (hours - 12).ToString() : hours) + (hours > 12 ? "AM" : "PM");
            // dayTimeText.text = (hours > 12 ? (hours - 12).ToString() : hours) + ":" + minutes.ToString("00") + (hours > 12 ? "AM" : "PM");
        } else {
            dayTimeText.text = "CLOSED";
        }
    }

    /// <summary>
    /// Link a Pizza p to some Order o. DayManager will remember this assignment.
    /// </summary>
    public void assignPizzaToOrder(Pizza p, Order o){ pizzaToOrder.Add(p, o); }
    /// <summary>
    /// Link a GameObject (represented by its GameObject.getInstanceID()) to some Pizza p. DayManager will remember this assignment.
    /// </summary>
    public void assignGameObjToPizza(int goID, Pizza p){ activePizzas.Add(goID, p); }
    /// <summary>
    /// Whatever Pizza the key goExisting (GameObjectID) is linked to, unassign this and assign it to goNew instead.
    /// </summary>
    public void swapGameObjPizza(int goExisting, int goNew){
        activePizzas.Add(goNew, activePizzas[goExisting]);
        activePizzas.Remove(goExisting);
    }

    /// <summary>
    /// Takes in a pizza GameObject ID linked to a Pizza in the DayManager dictionary. Submits this Pizza to the order it's linked to.
    /// Interacts with internal OrderManager and UI to evaluate and record the quality of this submission.
    /// </summary>
    public void submitOrder(int gameObjectID){
        if(!activePizzas.ContainsKey(gameObjectID)){
            Debug.LogWarning("GameObject not linked to pizza in dictionary");
            return;
        }
        Pizza p = activePizzas[gameObjectID];
        if(!pizzaToOrder.ContainsKey(p)){
            Debug.LogError("Pizza linked to GameObject not linked to Order");
            return;
        }
        Order o = pizzaToOrder[p];
        orderManager.submitOrder(o.getID(), p);
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
