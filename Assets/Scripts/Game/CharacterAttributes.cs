using System;

/*
*  Every character should have an instance of this class, basically keeping track of how good they are at different things.
*  Players should be able to choose their level progression themselves, AI will do it automatically.
*
*  Allocating leveling points can happen if the sum of attributes is not equal to the current level.
*  XP to level up just doubles each time, starting at 500 (about 5 orders)
*  
*/

// must be serializable for save system to work
[Serializable]
public class CharacterAttributes {
    // TODO: impleemnt some progression system
    private float xp;
    private int neededToLvlUp;
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

        xp = 0;
        lvl = 1;
        neededToLvlUp = 1000;
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

    public bool canLevelUp() {
        return lvl > (toss + top + ovens + cut + charisma + speed + stamina);
    }

    // takes in an order quality value and basically just adds it to the XP total
    public void addXP(float orderQuality) {
        xp += orderQuality;
        if(xp >= neededToLvlUp){
            xp -= neededToLvlUp;
            lvl++;
            neededToLvlUp *= 2;
        }
    }
}
