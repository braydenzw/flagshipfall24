using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PizzaScript : MonoBehaviour
{
    [Header("Scene")]
    public string kitchenScene = "KitchenScene";
    public TMP_Text toppingTxt;

    [Header("Game")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int toppingCount = 0;
    private Dictionary<Topping, List<GameObject>> toppingMap;
    private Dictionary<Topping, int> desiredToppings;
    private float timeDebug = 0;
    private float timeBetweenDebug = 1;
    public bool debugMode;
    public int typeCount = 3;
    public int maxNumberOfToppingsPerOrderedTopping = 3;

    private List<Topping> expectedToppings;

    void Start()
    {
        // hide player sprite for game
        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(false);

        toppingMap = new Dictionary<Topping, List<GameObject>>();

        // updated to make game based on current order associated with user 
        var dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();
        expectedToppings = dayManager.getUserOrder().expected.toppings;

        desiredToppings = generateOrder();
        
        // show player what they need to add
        string txt = "Toppings:\n";
        if(desiredToppings.Count > 0){
            foreach(KeyValuePair<Topping, int> entry in desiredToppings){
                if(entry.Key == Topping.Mushroom){
                    txt += entry.Value + " Mushroom\n";
                } else if(entry.Key == Topping.Pepperoni){
                    txt += entry.Value + " Pepperoni\n";
                } else {
                    txt += "err\n";
                }
            }
        } else {
            txt += " [ None ]\n";
        }
        toppingTxt.text = txt;
    }

    // Update is called once per frame
    void Update()
    {
        timeDebug += Time.deltaTime;
        if (debugMode && timeDebug > timeBetweenDebug)
        {
            timeDebug -= timeBetweenDebug;
            foreach (KeyValuePair<Topping, List<GameObject>> entry in toppingMap)
            {
                Debug.Log(entry.Key.ToString() + " " + entry.Value[0].transform.position);
            }
            foreach(KeyValuePair<Topping, int> entry in desiredToppings)
            {
                Debug.Log(entry.Key.ToString() + " " + entry.Value);
            }
        }
        // bool orderComplete = true;
        // foreach (KeyValuePair<Topping, int> entry in desiredToppings)
        // {
        //     if (entry.Value>0 && (!toppingMap.ContainsKey(entry.Key) || toppingMap[entry.Key].Count < entry.Value))
        //     {
        //         orderComplete = false;
        //     }
        // }
        // if(orderComplete)
        // {
        //     Debug.Log("Order Complete");
        //     // Do shit with score here I guess
        //     List<Topping> toppingList = new List<Topping>();
        //     foreach(KeyValuePair<Topping, List<GameObject>> entry in toppingMap) {
        //         for(int i = 0; i<entry.Value.Count; i++) {
        //             toppingList.Add(entry.Key);
        //         }
        //     }
            
        //     // END scene, restore player sprite, update pizza object
        //     var dm = GameObject.Find("DayManager").GetComponent<DayManager>();
        //     dm.addToppings(DayManager.PLAYER_ID, toppingList);

        //     GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
        //     SceneManager.LoadScene(kitchenScene);
        //     // debug print
        //     dm.printActivePizzas();
        // }

    }

    public void EndToppingGame() {
        List<Topping> toppingList = new List<Topping>();
        foreach(KeyValuePair<Topping, List<GameObject>> entry in toppingMap) {
            for(int i = 0; i<entry.Value.Count; i++) {
                if (!toppingList.Contains(entry.Key)){
                    toppingList.Add(entry.Key);
                }
            }
        }
        
        // END scene, restore player sprite, update pizza object
        var dm = GameObject.Find("DayManager").GetComponent<DayManager>();
        dm.addToppings(DayManager.PLAYER_ID, toppingList);

        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.LoadScene(kitchenScene);
        // debug print
        dm.printActivePizzas();
    }

    public void AddTopping(Topping toppingType, GameObject topping)
    {
        toppingCount++;
        if (!toppingMap.ContainsKey(toppingType))
        {
            toppingMap[toppingType] = new List<GameObject>();
        }
        toppingMap[toppingType].Add(topping);
    }
    public Dictionary<Topping, int> generateOrder() {
        Dictionary<Topping, int> order = new Dictionary<Topping, int>();
        foreach (Topping type in Enum.GetValues(typeof(Topping))) {
            order.Add(type,0);
        }
        // Generate typeCount random topping types to request
        // Give each a random # from 0-maxNumber
        for (int i = 0; i < expectedToppings.Count; i++) {
            // Topping randomType = (Topping)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Topping)).Length);

            // updated to be based on expected toppings
            // Topping randomType = expectedToppings[UnityEngine.Random.Range(1, expectedToppings.Count - 1)];
            int randomAmount = UnityEngine.Random.Range(1, maxNumberOfToppingsPerOrderedTopping);
            order[expectedToppings[i]] += randomAmount;
        }
        return order;
    }
    
}


