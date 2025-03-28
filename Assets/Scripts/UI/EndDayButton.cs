using UnityEngine;

public class EndDayButton : MonoBehaviour
{
    public void endDay()
    {
        var dm = GameObject.Find("DayManager");
        if(dm != null){
            dm.GetComponent<DayManager>().endDay();
        } else {
            Debug.LogError("Tried to end day with button, but could not find DayManager in scene.");
        }
    }
}
