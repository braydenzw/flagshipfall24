using UnityEngine;
using UnityEngine.SceneManagement;

/*
* Allows player to interact with toss station. This means activating the mini-game 
*  if the current game/player state should allow it.
*
*  Contributors: Caleb Huerta-Henry
*  Last Updated: March 8, 2025
*/

// TODO: right now this assumes the player will only interact w this station when facing Up

public class TossStation : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public string tossScene = "TossGame";
    public Color triggered = new Color(240f/255f, 6f/255f, 10f/255f, 0.2f);
    public Color untriggered = new Color(0f, 0f, 0f, 0.2f);

    private bool interactable = false;
    private SpriteRenderer tossTrigger;
    private GameObject playerPizza;
    private PlayerManager pm;

    void Start()
    {
        tossTrigger = GetComponent<SpriteRenderer>();
        tossTrigger.color = untriggered;

        var player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        playerPizza = player.transform.GetChild(0).gameObject;
        pm = player.GetComponent<PlayerManager>();
    }

    void Update()
    {
        if(interactable && !playerPizza.activeSelf) {
            tossTrigger.color = triggered;
            if(Input.GetKeyDown(interactKey)){
                SceneManager.LoadScene(tossScene);
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
            tossTrigger.color = untriggered;
        }
    }
}
