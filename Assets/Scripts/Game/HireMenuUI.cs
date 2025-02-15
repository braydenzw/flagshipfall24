using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HireMenuUI : MonoBehaviour
{
    // Add this singleton setup
    public static HireMenuUI Instance;

    [SerializeField] private Transform resumeContainer;
    [SerializeField] private GameObject resumePanelPrefab;

    [SerializeField] private Canvas hireMenuCanvas;

    void Awake()
    {
        // Initialize the singleton
        if (Instance == null)
        {
            Instance = this;
            hireMenuCanvas.enabled = false; // Disable the Canvas, not the GameObject
            Debug.Log("hire ui intiezlized");

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowHiringList(List<Employee> allStaff)
    {
        hireMenuCanvas.enabled = true;
        
        Debug.Log("Canvas enabled: " + hireMenuCanvas.enabled); // Should log "True"
        //gameObject.SetActive(true);
        foreach (Transform child in resumeContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Employee hero in allStaff)
        {
            GameObject panel = Instantiate(resumePanelPrefab, resumeContainer);
            ResumePanel resumePanel = panel.GetComponent<ResumePanel>();
            resumePanel.Setup(hero);
        }
    }

    public void HideHiringList()
    {
        hireMenuCanvas.enabled = false; // Hide the UI
        //gameObject.SetActive(true);
    }
}