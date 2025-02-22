using UnityEngine;

/*
* Script handling basic player controls and state variables
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Feb 22, 2025
*/

public class PlayerManager : MonoBehaviour
{
    private static GameObject plyr;
    public enum Direction {
        Up, Left, Right, Down
    };

    public float speed = 3f;
    private Rigidbody2D rb;

    private float horizInput, vertInput;
    private Direction dir = Direction.Up;
    public Direction facing() { return dir; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(transform.parent.gameObject);
        if (plyr == null) {
		    plyr = transform.parent.gameObject;
        } else {
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(horizInput * speed, vertInput * speed);

        if(Mathf.Abs(horizInput) > Mathf.Abs(vertInput) && horizInput != 0f){
            dir = horizInput > 0f ? Direction.Right : Direction.Left;
        } else if(vertInput != 0f) {
            dir = vertInput > 0f ? Direction.Up : Direction.Down;
        }
    }
}
