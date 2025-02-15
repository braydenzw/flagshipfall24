using System;
using UnityEngine;

/*
* This class should contain all info relevant to an employee
*  OR prospective hire AKA candidate employee
*
* Currently that includes:
*  1. Some identifier for the object
*  2. Some name viewable by the player
*  3. Current attributes, viewable by player
*  4. Current salary/pay
*  5. Cumulative earnings
*/

// must be serializable for save system to work
[CreateAssetMenu(fileName = "NewHero", menuName = "Hero/Create New Hero")]
[Serializable]
public class Employee : ScriptableObject
{
    private string id; // some unique ID
    public string name; // viewable name
    public CharacterAttributes attributes; // current stats
    public int salary; // current pay
    public int totalEarnings; // total earnings
    public Sprite sprite;

    public string description { get; internal set; }

    // TODO: implement instantiation function
    public Employee(){

    }

}
