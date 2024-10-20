using UnityEngine;

/*
* This is a generic object class for an "order"
*
* It should contain all relevant information for an order
*/

public class Order {
    private float orderID; // some way to ID order when it emits a value
    private float price; // can change to whatever data type we're using
    private Pizza expected; // some way to check if order submission is good
    private Time due; // can change type depending on implementation

    public Order(){
        // TODO: create instantiation function
    }

    // TODO: some way to "submit" pizza to order
        // specifically, this should emit some tuple with info (OrderID, Result[quality??, price])

    // TODO: some function to check quality/"grade" submission

    // TODO: getters to check due time, etc.
}
