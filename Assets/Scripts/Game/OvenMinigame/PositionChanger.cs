using UnityEngine;
using System.Collections;

public class PositionChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject hold;
    GameObject measure;
    private float angleChange;
    void Start()
    {
        hold = GameObject.Find("Square");
        measure = GameObject.Find("Measure");
    }

    // Update is called once per frame
    void Update()
    {
        angleChange = hold.GetComponent<Billow>().billowLevel - this.transform.localRotation.eulerAngles.z;
        this.transform.Rotate(0,0,angleChange, Space.Self);
        if(this.transform.localRotation.eulerAngles.z < -45)
        {
            measure.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
}
