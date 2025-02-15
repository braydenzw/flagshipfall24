using TMPro;
using UnityEngine;

// higher up the pizza is, the more downward it should project and vice versa
// at the start, should be fully horizontal

public class DoughMovement : MonoBehaviour
{
    private const int requiredTosses = 20;
    private const float speedMult = 10f; // how much speed increases per successful toss
    private const float baseHorizSpeed = 120f, baseVertSpeed = 80f, maxRotation = 5f;

    public GameObject dough, circular;
    public TossGameManager tossGame;
    public TMP_Text instructTxt;

    private Rigidbody2D rb;
    private float xStart = -4f, yStart = 0;
    private bool gameActive = false;

    private float maxSpeed = -1f, currSpeed = 0f;
    private bool direction = false; // true for right, false for right
    private float progress = 0f;
    private bool tossListener = false;

    void Start() {
        instructTxt.text = "Press [Space] to begin";
        rb = dough.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public bool tossStarted() { return progress > 0f; }
    public void beginGame(float maxSpeed) {
        instructTxt.text = "";
        dough.transform.position = new Vector3(xStart, yStart, 0);
        this.maxSpeed = maxSpeed;
        currSpeed = 1f;
        gameActive = true;
        toggleDoughMovement();
        doughMove();
    }
    public void continueGame() {
        if(!gameActive) {
            gameActive = true;
            direction = false;
            dough.transform.position = new Vector3(xStart, yStart, 0);
            toggleDoughMovement();
            doughMove();
        }
    }
    public void failGame() {
        toggleDoughMovement();
        tossListener = false;
        gameActive = false;
        dough.transform.position = new Vector3(0, -2.7f, 0);
        tossGame.tossFail();
        instructTxt.text = "Press [Space] to continue";
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
            tossListener = false;
            gameActive = false;
            dough.transform.position = Vector3.zero;
            tossGame.tossComplete();
        }

        if (tossListener) {
            var horiz = Input.GetAxis("Horizontal");
            if ((direction && horiz < 0f) || (!direction && horiz > 0f)){
                tossListener = false;
                progress += 100f / requiredTosses;

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
        }
    }
}
