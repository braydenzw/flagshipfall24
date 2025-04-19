using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Billow : MonoBehaviour
{
    public float billowLevel = 0;
    public float billowChanger = (float)1;
    GameObject measure;
    public Text cookText;
    public Text cookMaxText;
    int lowerLimit;
    int upperLimit;
    double cookedLevel = 0;
    double cookedMax = 100;
    int displayCookedMax;
    string cookedString = "";
    string cookedMaxString = "";
    int displayCook;
    private float timeRemaining = (float)0.7;
    private bool cooked;
    private bool cookingBegin = false;
    void Start()
    {
        measure = GameObject.Find("Measure");
        cooked = false;
    }
    
    void OnMouseDown()
    {
        if(timeRemaining <= 0 && billowLevel > -200)
        {
            timeRemaining = (float)0.3;
            billowChanger = -15;
            if(billowLevel == 0)
            {
                billowLevel = (float)-.001;
            }
        }
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(billowChanger < 1)
        {
            billowChanger += (float)1;
        }
        else if(billowChanger > 1)
        {
            billowChanger = (float)0.02;
        }
        if(billowLevel < 0)
        {
            billowLevel += billowChanger;
        }
        else if(billowLevel > 0)
        {
            billowLevel = 0;
        }

        if(billowLevel <= -60 && billowLevel >= -100)
        {
            cookingBegin = true;
            cookedLevel += .05;
        }
        if(cookingBegin == true && (billowLevel > -60 || billowLevel < -100))
        {
            cookedMax -= .05;
            displayCookedMax = (int)cookedMax;
            cookedMaxString = "" + displayCookedMax;
            cookMaxText.text = cookedMaxString;
        }
        {

        }
        if(cookedLevel >= cookedMax)
            {
                cooked = true;
            }
            displayCook = (int)cookedLevel;
            cookedString = displayCook + "";
            cookText.text = cookedString;
    }
}
