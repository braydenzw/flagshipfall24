using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayEndManager : MonoBehaviour
{
    private OrderManager om;

    [Header("Order Results")]
    public GameObject orderResultPrefab; // Name | Price: $xx.xx \n Tip: $xx.xx | Quality: xx
    public GameObject orderResultList; 

    [Header("Other Text")]
    public TMP_Text playerNameText;
    public TMP_Text dayResults; // Profit: $xx.xx | Avg Quality: xx.xx

    [Header("Scenes")]
    public string mainScene = "MainMenu";
    public string newDay = "KitchenScene";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var dayManager = GameObject.Find("DayManager");
        if(dayManager != null){
            om = dayManager.GetComponent<OrderManager>();
        } else {
            Debug.LogError("DayManager not found");
        }

        if(GameManager.gameData != null){
            playerNameText.text = GameManager.gameData.name;
        } else {
            Debug.LogError("GameManager.gameData not set up... cannot save");
        }

        // get order stats
        var orders = om.getOrders();
        var completedOrders = om.getCompletedOrders();
        var profit = 0f;
        var quality = 0f;

        foreach(OrderResult or in completedOrders){
            profit += or.getProfit();
            quality += or.getQuality();

            var newResult = Instantiate(orderResultPrefab);
            var resultString = orders[or.getOrderID()].customer + " | Price: $" + or.getPrice() + "\nTip: $" + or.getTip() + " | Quality: " + or.getQuality();
            newResult.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = resultString;
            newResult.transform.SetParent(orderResultList.transform);
        }

        dayResults.text = "Profit: $" + profit + " | Avg Quality: " + (quality / orders.Count);
        GameManager.saveDayResults(orders.Count, profit, quality);
    }

    public void loadNextDay() {
        // always destroy this, since new one is created each day
        Destroy(GameObject.Find("DayManager"));
        // new day loading should do all necessary setup itself
        SceneManager.LoadScene(newDay);
    }

    public void backToMain() {
        // still have to destroy this, since it belongs to the most recent day
        Destroy(GameObject.Find("DayManager"));
        SceneManager.LoadScene(mainScene);
    }
}
