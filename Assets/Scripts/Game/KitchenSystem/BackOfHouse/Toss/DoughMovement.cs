using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
* Script managing the actual functionality of the toss minigame. Handling dough movement, user input
*  for mini-game, fail vs success state, and game completion.
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Feb 22, 2025
*/

// TODO: when halfway through, allow a big spin upwards
    // maybe like right->up->left->down, gets you progress exactly matching lvl
    // (so like v high level gets a lot from the spin)

// TODO: use tossLvl to alter game (e.g. less requiredTosses, higher maxSpeed, larger collision objects)

public class DoughMovement : MonoBehaviour
{
    [Header("Movement/Game Vars")]
    public int requiredTosses = 20;
    public float speedMult = 10f; // how much speed increases per successful toss
    public float baseHorizSpeed = 120f, baseVertSpeed = 80f, maxRotation = 5f;

    [Header("Unity Objects")]
    public GameObject dough;
    public GameObject circular;
    public TossGameManager tossGame;
    public TMP_Text instructTxt;
    public TMP_Text qualityTxt;
    public Scrollbar progressBar;

    private Rigidbody2D rb;
    private float xStart = -4f, yStart = 0;
    private bool gameActive = false;

    private float maxSpeed = -1f, currSpeed = 0f;
    private bool direction = false; // true = right, false = left (toggles when game starts, so will start right)
    private float progress = 0f;
    private bool tossListener = false;

    void Start() {
        progressBar.gameObject.SetActive(false);
        qualityTxt.gameObject.SetActive(false);

        rb = dough.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public bool tossStarted() { return progress > 0f; }
    public void beginGame(float maxSpeed, int tossLvl) {
        // TODO: use tossLvl to alter game (e.g. less requiredTosses, higher maxSpeed, larger collision objects)
        instructTxt.text = "";
        qualityTxt.text = "Quality: 100";
        progressBar.size = 0f;
        qualityTxt.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);

        dough.transform.position = new Vector3(xStart, yStart, 0);
        this.maxSpeed = maxSpeed;
        currSpeed = 1f;
        StartCoroutine(gameInit());
    }
    private IEnumerator gameInit() {
        yield return new WaitForSeconds(1f);
        gameActive = true;
        toggleDoughMovement();
        doughMove();
    }

    public void continueGame() {
        if(!gameActive) {
            instructTxt.text = "";
            direction = false;
            dough.transform.position = new Vector3(xStart, yStart, 0);
            StartCoroutine(gameInit());
        }
    }
    public void failGame() {
        toggleDoughMovement();
        tossListener = false;
        gameActive = false;

        currSpeed = 1f;
        dough.transform.position = new Vector3(0, -2.7f, 0);
        tossGame.tossFail();
        instructTxt.text = "Press [Space] to continue...";
    }

    public void toggleDoughMovement() {
        if(rb.constraints == RigidbodyConstraints2D.None) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } else {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    public void doughMove(){
        direction = !direction;
        rb.linearVelocity = Vector2.zero;
        rb.AddTorque(Random.Range(-maxRotation, maxRotation));

        var x = (currSpeed * speedMult + baseHorizSpeed) * (direction ? 1f : -1f);
        var y = dough.transform.position.y < 1f ? baseVertSpeed * Random.Range(0.7f, 1.3f) : 0f;
        rb.AddForce(new Vector2(x, y));
        if(currSpeed < maxSpeed) {
            currSpeed++;
        }
    }

    void Update()
    {
        if (gameActive && progress >= 100f){
            // end game and send final quality to 
            toggleDoughMovement();
            progressBar.gameObject.SetActive(false);
            tossListener = false;
            gameActive = false;
            dough.transform.position = Vector3.zero;
            tossGame.tossComplete();
        }

        // TODO: if halfway through, allow a big spin upwards
            // maybe like right->up->left->down, gets you progress exactly matching lvl
            // (so like v high level gets a lot from the spin)

        if (tossListener) {
            var horiz = Input.GetAxis("Horizontal");
            if ((direction && horiz < 0f) || (!direction && horiz > 0f)){
                tossListener = false;
                progress += 100f / requiredTosses;
                progressBar.size = progress / 100f;

                // make pizza more circular each toss
                Vector3 scale = circular.transform.localScale;
                scale.x += 0.5f / requiredTosses;
                scale.y += 0.5f / requiredTosses;
                circular.transform.localScale = scale;
                doughMove();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.GetComponent<SpriteRenderer>().color = Color.red;
        tossListener = true; // allow user input to toss
    }
    void OnTriggerExit2D(Collider2D col)
    {
        col.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
        if (tossListener) {
            failGame();
            qualityTxt.text = "Quality: " + tossGame.getQuality();
        }
    }
}
