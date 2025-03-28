using System;
using System.Collections.Generic;
using UnityEngine;
public class PizzaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int toppingCount = 0;
    private Dictionary<Topping, List<GameObject>> toppingMap;
    private Dictionary<Topping, int> desiredToppings;
    private float timeDebug = 0;
    private float timeBetweenDebug = 1;
    public bool debugMode;
    public int typeCount = 3;
    public int maxNumberOfToppingsPerOrderedTopping = 3;
    private Pizza pizza;
    void Start()
    {
        toppingMap = new Dictionary<Topping, List<GameObject>>();
        desiredToppings=generateOrder(typeCount,maxNumberOfToppingsPerOrderedTopping);

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
        bool orderComplete = true;
        foreach (KeyValuePair<Topping, int> entry in desiredToppings)
        {
            if (entry.Value>0 && (!toppingMap.ContainsKey(entry.Key) || toppingMap[entry.Key].Count < entry.Value))
            {
                orderComplete = false;
            }
        }
        if(orderComplete)
        {
            Debug.Log("Order Complete");
            // Do shit with score here I guess
            List<Topping> toppingList = new List<Topping>();
            foreach(KeyValuePair<Topping, List<GameObject>> entry in toppingMap) {
                for(int i = 0; i<entry.Value.Count; i++) {
                    toppingList.Add(entry.Key);
                }
            }
            pizza.toppings = toppingList;
            // !!!! End scene somehow !!!!
        }

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
    public Dictionary<Topping, int> generateOrder(int typeCount, int maxNumber) {
        Dictionary<Topping, int> order = new Dictionary<Topping, int>();
        foreach (Topping type in Enum.GetValues(typeof(Topping))) {
            order.Add(type,0);
        }
        // Generate typeCount random topping types to request
        // Give each a random # from 0-maxNumber
        for (int i = 0; i < typeCount; i++) {
            Topping randomType = (Topping)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Topping)).Length);
            int randomAmount = UnityEngine.Random.Range(1, maxNumber);
            order[randomType] += randomAmount;
        }
        return order;
    }
    
}


