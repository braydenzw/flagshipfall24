using UnityEngine;
using UnityEngine.UI;
using static ToppingScript;
public class ToppingBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject topping;
    public int layerToCollide;
    public ToppingTypes toppingType;
    void Start()
    {
        layerToCollide=1<<layerToCollide;
    }

    // Update is called once per frame
    void Update()
    {
{

//do great stuff
}
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D click = Physics2D.Raycast(ray.origin, ray.direction,Mathf.Infinity,layerToCollide);
            if (click.collider != null) { 
                if (click.collider.gameObject == gameObject)
                {
                    Debug.Log ("CLICKED " + click.collider.name);
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    GameObject Topping = Instantiate(topping, new Vector3(mousePos.x,mousePos.y,-1.0f),Quaternion.identity);
                    Topping.GetComponent<ToppingScript>().toppingType=toppingType;
                }
            }
        }
    }
}
