using UnityEngine;

public class SetAnchor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.parent = GameObject.Find("rotationAnchor").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
