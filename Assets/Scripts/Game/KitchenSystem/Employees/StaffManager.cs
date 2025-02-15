/*
* This class should allow the player to manage their staff members.
*
* This means all roster management:
*  1. Firing current employees
*  2. Hiring new employees from available list
*  3. Managing shifts (who is assigned to what station. payment)
*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum Station {
    Toss, Top, Ovens, Cut, Orders
}

public class StaffManager : MonoBehaviour
{
    public static StaffManager Instance;
    [SerializeField] private List<Employee> allStaff = new List<Employee>();
    private List<Employee> currentHiringPool = new List<Employee>();

    private StaffData staffData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentHiringPool = new List<Employee>(); // STOP ERRORING here
            Debug.Log("StaffManager initialized");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public StaffManager(StaffData staffData){
        this.staffData = staffData;

        if(staffData == null){
            this.staffData = new StaffData();
        }
    }

    public void GenerateWeeklyHiringList(int numberOfCandidates)
    {
        if (HireMenuUI.Instance == null)
        {
            Debug.LogError("HireMenuUI.Instance not ready");
            return;
        }

        if (allStaff == null || allStaff.Count == 0) //CHECKING SUTFF BECAUSE WHY WONT THIS WORK
        {
            Debug.LogError("allHeroes list is empty or something");
            return;
        }

        currentHiringPool.Clear();
        List<Employee> tempList = new List<Employee>(allStaff);

        if (tempList.Count == 0)
        {
            Debug.LogError("FISDAHFIUASD YOU SHOULD NOT BE EEING THIS NO HEROES");
            return;
        }

        //randomly pick heroes
        for (int i = 0; i < Mathf.Min(numberOfCandidates, tempList.Count); i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            currentHiringPool.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex); // no duplicates in the same week
        }
        if (HireMenuUI.Instance == null)
        {
            Debug.LogError("HireMenuUI.Instance is not set!");
            return;
        }
        HireMenuUI.Instance.ShowHiringList(currentHiringPool);
        StartCoroutine(HideUIAfterDelay(5f));
    }

    private IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HireMenuUI.Instance.HideHiringList();
    }

    private List<Employee> hiredHeroes = new List<Employee>();

    public void HireHero(Employee hero)
    {
        if (!hiredHeroes.Contains(hero))
        {
            hiredHeroes.Add(hero);
            // Update gameplay stats 
            Debug.Log($"Hired {hero.name}!");
        }
    }

    public List<Employee> GetHiredHeroes()
    {
        return hiredHeroes;
    }

    public List<Employee> GetCurrentHiringPool()
    {
        return currentHiringPool;
    }
    // TODO: some function to change station assignment
    // TODO: some function to fire employee
    // TODO: some function to hire employee
    // TODO: some function to update payment
}
