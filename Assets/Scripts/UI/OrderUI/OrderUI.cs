using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Script managing UI of particular order in order list. 
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Feb 1, 2025
*/

// TODO: at some point, we will want to make this UI prettier

public class OrderUI : MonoBehaviour, IPointerClickHandler
{
    public Order order;
    public TMP_Text nameText, timeText, priceText, orderText;
    private float timeRemaining;
    private Color overdue = new Color(1f, 122 / 255f, 122 / 255f);

    public void setOrder(Order order, int index)
    {
        this.order = order;
        timeRemaining = order.timePlaced + order.timeAllowed - Time.time;
        nameText.text = index + ". " + order.customer;
        priceText.text = "<b>Price</b>: " + order.price;
        timeText.text = strTimeTilDue();
        orderText.text = "----------\n<b>Cook:</b> " + order.expected.cookLevel + "/100\n"
            + "<b>Cut</b>: " + order.expected.cutQuality + "/100\n"
            + "<b>Toppings</b>: " + order.toppingString();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining = order.timePlaced + order.timeAllowed - Time.time;
        timeText.text = strTimeTilDue();
        if(timeRemaining < 0 && gameObject.GetComponent<RawImage>().color != overdue) {
            gameObject.GetComponent<RawImage>().color = overdue;
        }
    }

    private string strTimeTilDue(){
        float secs = Mathf.Round(timeRemaining);
        if(secs >= 60f){
            float mins = Mathf.Floor(secs / 60);
            return mins + "m " + (secs % 60) + "s til due";
        } else {
            return secs + "s til due";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var tgm = GameObject.Find("TossManager").GetComponent<TossGameManager>();
        if(tgm != null){
            // try to assign this order to whatever pizza was just tossed
            tgm.assignToOrder(order);
        } else {
            Debug.LogError("OrderUI could not find TossManager object in scene");
        }
    }
}
