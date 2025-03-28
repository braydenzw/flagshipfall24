using System.Collections;
using System.Collections.Generic;
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
*   - Some time based updating of currently slotted Pizzas (cook them)
*/

public class OvensStation : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public string ovenScene = "OvenGame";
    public Color triggered = new Color(240f/255f, 6f/255f, 10f/255f, 0.2f);
    public Color untriggered = new Color(0f, 0f, 0f, 0.2f);

    private bool interactable = false;
    private SpriteRenderer trigger;
    private GameObject playerPizza;
    private PlayerManager pm;

    void Start()
    {
        trigger = GetComponent<SpriteRenderer>();
        trigger.color = untriggered;

        var player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        playerPizza = player.transform.GetChild(0).gameObject;
        pm = player.GetComponent<PlayerManager>();
    }

    void Update()
    {
        // must be in trigger and holding pizza object
        if(interactable && playerPizza.activeSelf) {
            trigger.color = triggered;
            if(Input.GetKeyDown(interactKey)){
                Debug.Log("Loading oven game");
                SceneManager.LoadScene(ovenScene);
            }
        }
    }

    // only allow game activation based on collider trigger
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
