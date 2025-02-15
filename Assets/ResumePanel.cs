using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResumePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    //[SerializeField] private TMP_Text friendlinessText;
    [SerializeField] private TMP_Text salaryText;
    [SerializeField] private Button hireButton;
    [SerializeField] private Image characterImage;

    private Employee currentHero;

    public void Setup(Employee hero)
    {
        currentHero = hero;
        nameText.text = hero.name;
        descriptionText.text = hero.description;
        //friendlinessText.text = $"Friendliness: {hero.friendliness}";
        salaryText.text = $"Salary: ${hero.salary}/week";
        characterImage.sprite = hero.sprite;

        hireButton.onClick.AddListener(OnHireClicked);
    }

    private void OnHireClicked()
    {
        StaffManager.Instance.HireHero(currentHero);
        Destroy(gameObject); // Remove the resume from the UI
    }
}