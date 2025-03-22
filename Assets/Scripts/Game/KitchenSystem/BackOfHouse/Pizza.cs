using System.Collections.Generic;
using UnityEngine.Rendering;

/*
* This is a generic object class for "Pizzas"
*
* It should contain all relevant data for a pizza object!
* Pizzas are defined by several factprs:
*  1. Toss Quality (should be known on creation)
*  2. Toppings (Some list of items)
*  3. Cook Level (some range of num.s that can be updated)
*  4. Cut Type (in what way is the pizza being cut)
*  5. Cut Quality (some range of num.s)
*/

// Container to have consistent topping references
public enum Topping {
    // TODO: create list of toppings
    Pepperoni,
    Mushroom,
}

public enum CutType {
    // TODO: finalize list of cut types
    None,
    Sixths, Eighths, Sixteenths,
    Breadstick, Square
}

public class Pizza {
    private int id; // some identifier (unique!)
    private float tossQuality;
    private List<Topping> toppings; // some container for current toppings
    private float cookLevel;
    private float cutQuality;
    private CutType cutType;

    public Pizza(float toss){
        id = -1; // TODO: MAKE THIS UNIQUE and generate on init()
        tossQuality = toss;
        toppings = null; // init to empty list

        cookLevel = 0; // uncooked

        cutQuality = 0;
        cutType = CutType.None; // un cut
    }

    // TODO: some way to add toppings
    // TODO: some way to update cookLevel
    // TODO: some way to update cut quality
}
