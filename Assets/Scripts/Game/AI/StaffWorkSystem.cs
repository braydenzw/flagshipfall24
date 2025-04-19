using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaffWorkSystem : MonoBehaviour
{
    public class StaffTimer {
        public Coroutine coroutine;
        public Action onComplete;
    }

    private static GameObject staffWorkSystem;
    public string mainScene = "KitchenScene";

    public StaffManager staffManager;
    private Dictionary<string, Coroutine> activeWork = new Dictionary<string, Coroutine>();

    void Start()
    {
        var smObj = GameObject.Find("StaffManager");
        if(smObj != null) {
            staffManager = smObj.GetComponent<StaffManager>();
        }

        DontDestroyOnLoad(this.gameObject);
        if (staffWorkSystem == null) {
		    staffWorkSystem = this.gameObject;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == mainScene) {
            // wait for pings from staff movement script to start work timers

        } else {
            foreach(KeyValuePair<string, Station> entry in staffManager.getStaffData().stationAssignments){
                if(!activeWork.ContainsKey(entry.Key)){
                    // start new work
                    StartCoroutine(doEmployeeWork(staffManager.getStaffData().getEmployeeByID(entry.Key), entry.Value));
                }
            }
        }
    }

    private IEnumerator doEmployeeWork(Employee e, Station s) {
        switch(s){
            case Station.Toss:
                // wait for new orders
                // wait for seconds based on toss skill
                // add pizza to first empty slot of toss table
                break;
            case Station.Top:
                // wait for pizzas at toss table
                // move pizza to top table
                // wait for seconds based on top skill
                break;
            case Station.Ovens:
                // wait for pizzas at top table
                // move pizza to oven
                // wait for seconds based on oven skill
                // move pizza to cut table
                break;
            case Station.Cut:
                // wait for pizzas at cut table
                // wait for secpomnds based on cut skill
                // complete order
                break;
        }
        return null;
    } 

    public void stopEmployeeWork(string id){
        if(activeWork.ContainsKey(id)) {
            StopCoroutine(activeWork[id]);
            activeWork.Remove(id);
        }
    }
}
