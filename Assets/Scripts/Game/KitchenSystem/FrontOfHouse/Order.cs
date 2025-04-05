using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

/*
* This is a generic object class for an "order"
*
* It should contain all relevant information for an order
*  And should handle order submission
*
* Contributors: Caleb Huerta-Henry
*  Last Updated: 1 Feb 2025
*/

public class Order {
    // at some point we could make these dependent on customers
        // aka variable input on creation, but for now they're just const
    
    // constants to reference in evaluating order score
    const double tossWeight = 0.1;
    const double topWeight = 0.4;
    const double cookWeight = 0.2;
    const double cutWeight = 0.1;
    const double timeWeight = 0.2;

    private string id; // some way to ID order when it emits a value
    public Pizza expected; // some way to check if order submission is good
    public string customer; // customer name
    public float price; // can change to whatever data type we're using
    public float timePlaced; // keep track of when order was placed
    public float timeAllowed; // and when it is expected

    public Order(string customer, float price, Pizza expected, float timePlaced, float timeAllowed){
        id = Guid.NewGuid().ToString();
        this.customer = customer;
        this.price = price;
        this.expected = expected;
        this.timePlaced = timePlaced;
        this.timeAllowed = timeAllowed;
    }
    public Order(Order copy){
        id = Guid.NewGuid().ToString();
        customer = copy.customer;
        price = copy.price;
        expected = copy.expected;
        timePlaced = copy.timePlaced;
        timeAllowed = copy.timeAllowed;
    }

    public string getID(){
        return id;
    }

    public string toppingString(){
        if(expected.toppings.Count == 0){
            return "None";
        } else {
            string ret = "";
            foreach(Topping t in expected.toppings){
                ret += t.ToString() + ", ";
            }
            ret = ret.Substring(0, ret.Length - 2);
            return ret;
        }
    }

    // handle submissions of orders, which must contain some Pizza object to evaluate
    public OrderResult submit(Pizza p){
        // compare to expected to evaluate score
        // toss score as binary
        int tossScore = p.tossQuality >= expected.tossQuality ? 1 : 0;
        // toppings list as 1 - (# incorrect / total)
        double numMissing = expected.toppings.Except(p.toppings).Count();
        double numExtra = p.toppings.Except(expected.toppings).Count();
        double pctIncorrect = (numMissing + numExtra) / expected.toppings.Count();
        double topScore = Math.Max(1.0 - pctIncorrect, 0); // min is 0 (no negatives)
        // cook score as extent of diff from expected (100 is range, )
        float cookScore = (100f - Math.Abs(expected.cookLevel - p.cookLevel)) / 100f;
        // cut quality same as toss
        int cutQuality = expected.cutQuality >= p.cutQuality ? 1 : 0;
            // also needs to be same type
        int cutType = expected.cutType == p.cutType ? 1 : 0;

        // also consider time -> past time = penalty to score
            // currently imore than twice as long as expected is a score of 0
            // TODO: if allowing for pause, take that into account here...
        float timeScore = 1f - Math.Min(1f, Math.Max(0, timePlaced + timeAllowed - Time.time) / timeAllowed);

        // score is out of 100
        int score = (int)(100.0 * (tossWeight * tossScore
            + topWeight * topScore
            + cookWeight * cookScore
            + cutWeight * (0.5 * cutQuality + 0.5 * cutType)
            + timeWeight * timeScore));
        float tip = price * tipPercentage(score);

        Debug.Log("TOTAL SCORE: " + score + " // TIP: " + tip);

        return new OrderResult(id, price, tip, score);
    }
    private float tipPercentage(int quality){
        if(quality > 90){
            return 0.2f;
        } else if (quality > 80){
            return 0.15f;
        } else if (quality > 70){
            return 0.1f;
        } else if (quality > 60){
            return 0.05f;
        } else {
            return 0f;
        }
    }
}

// helper struct to organize result data returned
public struct OrderResult {
    readonly string orderID;
    readonly float price;
    readonly float tip;
    readonly int quality;

    public OrderResult(string orderID, float price, float tip, int quality){
        this.orderID = orderID;
        this.price = price;
        this.tip = tip;
        this.quality = quality;
    }

    public string getOrderID() { return orderID; }
    public float getProfit() { return price + tip; }
    public float getQuality() { return quality; }
    public float getPrice() { return price; }
    public float getTip() { return tip; }
}