/*
* This is a generic class containing all information and interaction
*  with the kitchen components.
* 
*  Last Updated 1 Feb 2025
*  Contibutors: Caleb Huerta-Henry
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen {
    private List<GameObject> claimedPizzas; // pizza IDs
    private Dictionary<GameObject, Pizza> pizzaObjects; // pizza ID to associated GameObject in scene

    public Kitchen(){
        claimedPizzas = new List<GameObject>();
        pizzaObjects = new Dictionary<GameObject, Pizza>();
    }

    /// <summary>
    /// Attempts to claim pizza data object through associated GameObject
    /// </summary>
    /// <returns>
    /// The Pizza data object associated with the claimed GameObject or null if action is not valid (already claimed or system error).
    /// </returns>
    public Pizza claimPizza(GameObject p){
        if(claimedPizzas.Contains(p)){
            return null;
        } else if(pizzaObjects.ContainsKey(p)) {
            claimedPizzas.Add(p);
            return pizzaObjects[p];
        } else {
            Debug.LogError("Attempted to claim pizza without associated GameObject.");
            return null;
        }
    }
    public bool unclaimPizza(GameObject p){
        return claimedPizzas.Remove(p);
    }

    public void addPizza(GameObject o, Pizza p){
        pizzaObjects.Add(o, p);
    }
    public bool removePizza(GameObject p){
        return pizzaObjects.Remove(p) && claimedPizzas.Remove(p);
    }
}
