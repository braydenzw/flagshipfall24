using UnityEngine;
using UnityEngine.SceneManagement;

/*
* Script managing internals of the toss mini-game. Minigame initialization, assigning orders to created pizza objects,
*  and viewing current orders.
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Feb 22, 2025
*/

public class TossGameManager : MonoBehaviour
{
    // consts
    private const string mainScene = "TestEnviroment"; // TODO: update this when we have a real scene
    private const float baseSpeed = 7f;
    private const float penalty = 5f; // quality decrease per failure
    private const KeyCode orderUIKey = KeyCode.E;

    public DoughMovement dough;
    public GameObject orderUI, orderAssign;

    private DayManager dayManager; // used to see existing orders
    private int tossLevel = 1; // player skill lvl from loaded game data

    private bool gameActive = false, gameEnded = false;
    private float quality = 100f; // 0-100, used when creating new pizza object
    private Pizza created;
    private float maxSpeed = -1f;

    public float getQuality(){ return quality; }

    void Start() {
        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(false);
        dayManager = GameObject.Find("DayManager").GetComponent<DayManager>();
        maxSpeed = baseSpeed;
        if(GameManager.gameData != null && GameManager.gameData.playerStats != null) {
            tossLevel = GameManager.gameData.playerStats.attr.toss;
            maxSpeed += tossLevel;
        } else {
            Debug.LogWarning("No game save data found");
        }
    }

    void Update()
    {
        if(!orderUI.activeSelf && !gameActive && Input.GetKeyDown(KeyCode.Space)){
            if(!dough.tossStarted()) {
                dough.beginGame(maxSpeed, tossLevel);
                gameActive = true;
            } else {
                dough.continueGame();
            }
        }
        if(!gameActive && !gameEnded && !dough.tossStarted() && Input.GetKeyDown(orderUIKey)){
            orderUI.SetActive(!orderUI.activeSelf);
        }
        if(!gameEnded && Input.GetKeyDown(KeyCode.Escape)){
            GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
            SceneManager.LoadScene(mainScene);
        }
    }

    public void tossFail(){
        quality -= penalty;
        gameActive = false;
    }
    public void tossComplete() {
        gameEnded = true;
        created = new Pizza(quality);

        // toggle orderUI with assignment options
        orderAssign.SetActive(true);
        orderUI.SetActive(true);
    }

    // TODO: this will create a new pizza GameObject and link it to the created Pizza data object
        // It will also assign this to an order (note: orders should be allowed to be assigned to an infinite # of pizzas)
        // so the person who gets it (ai or human) knows what to do with it
    public void assignToOrder(Order o){
        if(gameEnded) {
            var playerPizza = GameObject.Find("Player").transform.GetChild(0).GetChild(0).gameObject;
            dayManager.assignPizzaToOrder(created, o);
            dayManager.assignGameObjToPizza(playerPizza.GetInstanceID(), created);
            
            playerPizza.SetActive(true);
            GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);
            SceneManager.LoadScene(mainScene);
        }
    }
}
