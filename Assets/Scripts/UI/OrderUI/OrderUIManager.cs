using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
* Class handling order UI updates/visuals (e.g. creates and manages UI objecs to be shown in list)
*
* Contributors: Caleb Huerta-Henry
* Last Updated: Jan 26, 2025
*/

public class OrderUIManager : MonoBehaviour
{
    public GameObject orderList;
    public GameObject orderPrefab;
    private OrderManager orderManager;
    private Dictionary<Order, GameObject> uiOrders = new Dictionary<Order, GameObject>();
    private int numOrders = 0;

    void Start() {
        orderManager = GameObject.Find("DayManager").GetComponent<OrderManager>();
    }
    void Update()
    {
        // TODO: technically if a remove and add happen in the same frame, this could break...
            // so probably needs to be a Set comparison instead
        if(gameObject.activeSelf && orderList.transform.childCount != orderManager.numOrders()) {
            foreach(Order o in orderManager.getOrders().Values) {
                if(!uiOrders.ContainsKey(o)){
                    var newOrder = Instantiate(orderPrefab);
                    numOrders++;
                    newOrder.GetComponent<OrderUI>().setOrder(o, numOrders); // init needed data

                    // use binary search to place, since rest of children are guaranteed to be sorted already
                    float search = o.timePlaced + o.timeAllowed;
                    int left = 0, right = orderList.transform.childCount;
                    while (left < right) {
                        int mid = (left + right) / 2;
                        Order temp = orderList.transform.GetChild(mid).GetComponent<OrderUI>().order;
                        if ((temp.timePlaced + temp.timeAllowed) <= search){
                            left = mid + 1;
                        } else {
                            right = mid;
                        }
                    }
                    
                    newOrder.transform.SetParent(orderList.transform); // attach to order list
                    newOrder.transform.SetSiblingIndex(left); // place sorted by time remaining
                    uiOrders.Add(o, newOrder); // keep track of in here
                }
            }
            foreach(Order o in uiOrders.Keys){
                if(!orderManager.getOrders().ContainsValue(o)){
                    Destroy(uiOrders[o]);
                    uiOrders.Remove(o);
                }
            }
        }
    }
}
