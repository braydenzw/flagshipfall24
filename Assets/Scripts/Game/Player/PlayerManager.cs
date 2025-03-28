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
    
    public float speed = 3f;
    private Rigidbody2D rb;

    private float horizInput, vertInput;

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
    }
}
