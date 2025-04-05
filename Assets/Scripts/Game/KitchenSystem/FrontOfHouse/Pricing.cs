using UnityEngine;

/*
* This class should just keep track of any pricing stuff and mapping toppings
*  to some price (can be expanded as needed)
*/

public static class Pricing
{   
    public static float toppingPrice(Topping t){
        switch(t){
            // TODO: finalize topping prices
            case Topping.Pepperoni:
                return 1.5f;
            case Topping.Mushroom:
                return 1f;
            default:
                Debug.LogWarning("Could not find price for unrecognized topping.");
                return 0;
        }
    }
}
