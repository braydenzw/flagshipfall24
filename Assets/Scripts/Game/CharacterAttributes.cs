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
    public int lvl;

    // pizza attributes
    public int toss;
    public int top;
    public int ovens;
    public int cut;

    // general attributes
    public int charisma;
    public int speed;
    public int stamina;

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

    // TODO: add some progression system on "lvl up"
}
