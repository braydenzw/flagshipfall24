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
            case Topping.Mushrooms:
                return 1f;
            case Topping.Sausage:
                return 2f;
            default:
                Debug.LogWarning("Could not find price for unrecognized topping.");
                return 0;
        }
    }
}
