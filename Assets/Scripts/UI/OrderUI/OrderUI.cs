using System;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
* Script managing UI of particular order in order list. 
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Jan 26, 2025
*/

// TODO: at some point, we will want to make this UI prettier

public class OrderUI : MonoBehaviour
{
    public Order order;
    public TMP_Text nameText;
    public TMP_Text timeText;
    public TMP_Text priceText;
    public TMP_Text orderText;

    private float timeRemaining;
    private Color overdueColor = new Color(1f, 122 / 255f, 122 / 255f);

    public void setOrder(Order order, int index)
    {
        this.order = order;
        timeRemaining = order.timePlaced + order.timeAllowed - Time.time;
        nameText.text = index + ". " + order.customer;
        priceText.text = "<b>Price</b>: " + order.price;
        timeText.text = strTimeTilDue();
        orderText.text = "----------\n<b>Cook:</b> " + order.expected.cookLevel + "/100\n"
            + "<b>Cut</b>: " + order.expected.cutType + "\n"
            + "<b>Toppings</b>: " + order.toppingString();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining = order.timePlaced + order.timeAllowed - Time.time;
        timeText.text = strTimeTilDue();
        if(timeRemaining < 0 && gameObject.GetComponent<RawImage>().color != overdueColor) {
            gameObject.GetComponent<RawImage>().color = overdueColor;
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
}
