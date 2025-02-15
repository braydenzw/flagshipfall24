using UnityEngine;

public class TossGameManager : MonoBehaviour
{
    // consts
    private const float baseSpeed = 5f;
    private const float penalty = 5f; // quality decrease per failure


    public DoughMovement dough;
    private DayManager dayManager;

    private bool gameActive = false;
    private float quality = 100f; // 0-100, used when creating new pizza object
    private float maxSpeed = -1f;

    void Start() {
        dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();
        maxSpeed = baseSpeed;
        if(GameManager.gameData != null && GameManager.gameData.playerStats != null) {
            maxSpeed += GameManager.gameData.playerStats.attr.toss;
        } else {
            Debug.LogWarning("No game save data found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameActive && Input.GetKeyDown(KeyCode.Space)){
            if(!dough.tossStarted()) {
                dough.beginGame(maxSpeed);
                gameActive = true;
            } else {
                dough.continueGame();
            }
        }
    }

    public void tossFail(){
        quality -= penalty;
        gameActive = false;
    }
    public void tossComplete() {
        // use quality to generate new pizza object
        Debug.Log("TOSS COMPLETED w/ QUALITY: " + quality);
    }

    // TODO: this will create a new pizza GameObject and link it to the created Pizza data object
        // It will also assign this to an order (note: orders should be allowed to be assigned to an infinite # of pizzas)
        // so the person who gets it (ai or human) knows what to do with it
    public void assignToOrder(){

    }
}
