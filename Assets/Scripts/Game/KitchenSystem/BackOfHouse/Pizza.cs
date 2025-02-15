using System;
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
    Pepperoni, Mushrooms, Sausage
}

public enum CutType {
    // TODO: finalize list of cut types
    None,
    Sixths, Eighths, Sixteenths,
    Breadstick, Square
}

public class Pizza {
    private string id; // unique identifier
    public float tossQuality;
    public List<Topping> toppings; // some container for current toppings
    public float cookLevel; // 0-100 value [50 as extra light, 70 as light, 80 as normal, 90 as well done, 100 as extra well done]
    public float cutQuality;
    public CutType cutType;

    // this instance function is for generating expected pizzas
    public Pizza(float toss, List<Topping> top, float cook, float cut, CutType cutType){
        tossQuality = toss;
        toppings = top;
        cookLevel = cook;
        cutQuality = cut;
        this.cutType = cutType;
    }

    public string getID(){
        return id;
    }

    // this is for generating a default pizza object through the toss mini-game
    public Pizza(float toss){
        id = Guid.NewGuid().ToString();
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
