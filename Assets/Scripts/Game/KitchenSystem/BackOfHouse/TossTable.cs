using UnityEngine;

/*
* Essentially creates several places to set down and pick up existing pizza objects.
*  Interactable based on a trigger collider.
*
*  Contributors: Caleb Huerta-Henry
*  Last Updated: March 8, 2025
*/


public class TossTable : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public Color triggered = new Color(240f/255f, 6f/255f, 10f/255f, 0.2f);
    public Color untriggered = new Color(0f, 0f, 0f, 0.2f);
    public string stationName = "TossTable";
    
    private bool interactable = false;
    private SpriteRenderer trigger;
    private GameObject pizza;
    private GameObject player, playerPizza;
    private PlayerManager pm;
    private DayManager dm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pizza = this.transform.GetChild(0).gameObject;
        trigger = GetComponent<SpriteRenderer>();
        trigger.color = untriggered;

        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        playerPizza = player.transform.GetChild(0).gameObject;
        pm = player.GetComponent<PlayerManager>();

        dm = GameObject.Find("DayManager").GetComponent<DayManager>();
        pizza.SetActive(dm.pizzaExistsWithID(stationName));
    }

    void Update()
    {
        // either player has no pizza or table has no pizza (not both)
        if(interactable && playerPizza.activeSelf != pizza.activeSelf){
            trigger.color = triggered;
            if(Input.GetKeyDown(interactKey)){
                if(playerPizza.activeSelf){
                    // move player pizza to table
                    dm.swapGameObjPizza(DayManager.PLAYER_ID, stationName);
                    playerPizza.SetActive(false);
                    pizza.SetActive(true);
                } else {
                    // claim pizza from table
                    dm.swapGameObjPizza(stationName, DayManager.PLAYER_ID);
                    playerPizza.SetActive(true);
                    pizza.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player"){
            interactable = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player"){
            interactable = false;
            trigger.color = untriggered;
        }
    }
}
