using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This is an actual Unity script that should allow the player to interact with this station
*  The actual implementation of this is up to whoever is scripting this...
*  
*  Requirements:
*   - User can interact with the station somehow
*   - When interacting, user can:
*       - Place a pizza down in an unoccupied slot
*       - Pick up a pizza from an occupied slot
*       - Begin some mini-game related to the station
*   - Based on the performance in mini-game, Pizza object is updated with addTopping() function (actual name might be diff)
*/


public class TopStation : MonoBehaviour
{
    public KeyCode swapPizzaKey = KeyCode.Q;
    public KeyCode startGameKey = KeyCode.E;
    public string topScene = "TopGame";
    public Color triggered = new Color(240f/255f, 6f/255f, 10f/255f, 0.2f);
    public Color untriggered = new Color(0f, 0f, 0f, 0.2f);
    
    private bool interactable = false;
    private SpriteRenderer trigger;
    private GameObject pizza, player, playerPizza;
    private PlayerManager pm;
    private DayManager dm;

    // Start is called before the first frame update
    void Start()
    {
        pizza = this.transform.GetChild(0).gameObject;
        trigger = GetComponent<SpriteRenderer>();
        trigger.color = untriggered;

        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        playerPizza = player.transform.GetChild(0).gameObject;
        pm = player.GetComponent<PlayerManager>();
        dm = GameObject.Find("DayManager").GetComponent<DayManager>();
    }

    void Update()
    {
        // should only be able to swap or load mini-game
        if(interactable && playerPizza.activeSelf != pizza.activeSelf) {
            trigger.color = triggered;
            if(Input.GetKeyDown(swapPizzaKey)){
                if(playerPizza.activeSelf){
                    // move player pizza to table
                    dm.swapGameObjPizza(playerPizza.GetInstanceID(), pizza.GetInstanceID());
                    playerPizza.SetActive(false);
                    pizza.SetActive(true);
                } else {
                    // claim pizza from table
                    dm.swapGameObjPizza(pizza.GetInstanceID(), playerPizza.GetInstanceID());
                    playerPizza.SetActive(true);
                    pizza.SetActive(false);
                }
            }
        } else if(interactable && (playerPizza.activeSelf || pizza.activeSelf)) {
            trigger.color = triggered;
            if(Input.GetKeyDown(startGameKey)){
                Debug.Log("Loading top scene");
                SceneManager.LoadScene(topScene);
            }
        }
    }

    // only allow game activation based on collider trigger
    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player"){
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = true;
            Collider2D[] results = new Collider2D[10];

            // If already in active trigger point, don't trigger any other ones
            if(col.Overlap(filter, results) <= 1){
                interactable = true;
            }
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
