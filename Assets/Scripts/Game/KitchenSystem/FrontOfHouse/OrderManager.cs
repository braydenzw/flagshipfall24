/*
* This class should be used by the DayProgression during each day to generate
*  and handle orders
*
* This class should have containers of Order objects which can be retrieved
*  by other classes/scripts
*
* Contributors: Caleb Huerta-Henry
*  Last Updated: 26 Jan 2025
*/

using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour {

    // default mins/maxes for order and pizza stats
    private const float minTossQuality = 50;
    private const float maxTossQuality = 100;
    private const int minToppings = 0;
    private const int maxToppings = 5;
    private const float minCookLevel = 50;
    private const float maxCookLevel = 100;
    private const float minCutQuality = 50;
    private const float maxCutQuality = 100;
    private const float minTimeAllowed = 60 * 1.5f; // TODO: finalize these times bc idk what is reasonable
    private const float maxTimeAllowed = 60 * 4f; // TODO: this one too
    private const float basePrice = 20f;

    // container of all orders for current day
    private Dictionary<string, Order> orders;
    // container of completed orders for this day
    private List<OrderResult> completedOrders;

   private void Start() {
        orders = new Dictionary<string, Order>();
        completedOrders = new List<OrderResult>();
    }
    public Dictionary<string, Order> getOrders() {
        return orders;
    }
    public int numOrders() {
        return orders.Count;
    }

    // some way to submit to a order id
    public OrderResult submitOrder(string orderID, Pizza p){
        Order order = orders[orderID];

        if(order != null){
            OrderResult res = order.submit(p);
            completedOrders.Add(res);
            orders.Remove(orderID);
            return res;
        }

        Debug.LogError("Attempted to submit order with invalid orderID");
        return new OrderResult(orderID, -1, -1);
    }

    // generate a random order based on current player LVL
    public Order generateOrder(PlayerStats ps, string customer){
        int lvl = ps.attr.lvl; // difficulty of generated order depends on curr player lvl
        float price = basePrice;

        float toss = randomBellCurve(minTossQuality + lvl, maxTossQuality);
        List<Topping> toppings = new List<Topping>();
        int numToppings = Random.Range(minToppings, maxToppings);
        for(int i = 0; i < numToppings; i++){
            Topping t = (Topping) Random.Range(0, System.Enum.GetNames(typeof(Topping)).Length);
            if(!toppings.Contains(t)){
                toppings.Add(t);
                price += Pricing.toppingPrice(t);
            }
        }

        // cook level is more stylistic than difficulty, so not based on lvl
        float cook = randomBellCurve(minCookLevel, maxCookLevel);
        float cut = randomBellCurve(minCutQuality + lvl, maxCutQuality);
        CutType cutType = (CutType) Random.Range(0, System.Enum.GetNames(typeof(CutType)).Length);

        // random time allowed w/ difficulty based on lvl
        float timeAllowed = randomBellCurve(minTimeAllowed, maxTimeAllowed - lvl);
        Order newOrder = new Order(customer, price,
            new Pizza(toss, toppings, cook, cut, cutType),
            Time.time, timeAllowed);
        orders.Add(newOrder.getID(), newOrder);
        return newOrder;
    }

    // generate a random num in range with bell curve distribution (aka bias towards middle values)
    public float randomBellCurve(float min, float max){
        return Mathf.Round((Random.Range(min, max) + Random.Range(min, max)) / 2f);
    }
}
