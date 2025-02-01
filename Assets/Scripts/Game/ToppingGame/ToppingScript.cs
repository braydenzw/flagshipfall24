using UnityEngine;
using UnityEngine.Rendering;

public class ToppingScript : MonoBehaviour
{
    public int pizzaLayer;

    bool isHeld;
    
    public GameObject pizza;
    
    public enum ToppingTypes
    {
        Pepperoni,
        Dick,
    }
    public ToppingTypes toppingType;
    void Start()
    {
        pizza = GameObject.Find("Pizza");
        pizzaLayer=1<<pizzaLayer;
        isHeld=true;
        switch (toppingType)
        {
            // This is where you assign the color of each topping type
            case ToppingTypes.Pepperoni:
                this.GetComponent<SpriteRenderer>().color=Color.red;
                break;
            case ToppingTypes.Dick:
                this.GetComponent<SpriteRenderer>().color=Color.black;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position=new Vector3(mousePos.x,mousePos.y,this.transform.position.z);
            if(!Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D dropRay = Physics2D.Raycast(ray.origin, ray.direction,Mathf.Infinity,pizzaLayer);
                isHeld=false;
                if(!dropRay) Destroy(this.gameObject);
                else {
                    
                }
            }
        }
        else
        {
            //wtf was I going to do with this else statement????
            //idk but I will keep it maybe I will remember :)
        }
    }
}
