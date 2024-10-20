using System;

/*
* This class should be very hefty!
*
* Every character should have an instance of this class.
* This class should contain setters and getters for every attribute.
* 
*/

// must be serializable for save system to work
[Serializable]
public class CharacterAttributes {
    // TODO: impleemnt some progression system
    private int xp;
    private int lvl;

    // pizza attributes
    private int toss;
    private int top;
    private int ovens;
    private int cut;

    // general attributes
    private int charisma;
    private int speed;
    private int stamina;

    public CharacterAttributes(){
       // init to 0
       toss = 0;
       top = 0;
       ovens = 0;
       cut = 0;
       
       charisma = 0;
       speed = 0;
       stamina = 0;
    }
    
    public CharacterAttributes(int toss, int top, int ovens, int cut, int charisma, int speed, int stamina, int xp){
        this.toss = toss;
        this.top = top;
        this.ovens = ovens;
        this.cut = cut;
        
        this.charisma = charisma;
        this.speed = speed;
        this.stamina = stamina;

        this.xp = xp;
    }

    // TODO: add getters and setters

    // TODO: add some progression system on "lvl up"
}
