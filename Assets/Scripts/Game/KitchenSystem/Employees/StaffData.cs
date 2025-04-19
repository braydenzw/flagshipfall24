using System;
using System.Collections.Generic;
using System.Linq;

/*
* This class should contain all data that should be saveable of the staff
*  This will be used to read/write save files (serializable)
*
* Currently this should just be:
*  1. A list of all Employee objects currently hired
*  2. A Map<EmployeeID, Station> of station assignments 
*  3. A Map<EmployeeID, Payment> of current payment
*
* Note: Station enum is defined in StaffManager.cs
*
* Contributors: Caleb Huerta-Henry
* Last Updated: 10/20/2024
*/

[Serializable]
public class StaffData {
    
    // list of generated prospects? 

    public List<Employee> employees;
    public Dictionary<string, Station> stationAssignments;
    public Dictionary<string, double> employeePayment;

    public StaffData(){
        employees = new List<Employee>();
        stationAssignments = new Dictionary<string, Station>();
        employeePayment = new Dictionary<string, double>();
    }

    public StaffData(List<Employee> employees, Dictionary<string, Station> stationAssignments, Dictionary<string, double> employeePayment){
        this.employees = employees;
        this.stationAssignments = stationAssignments;
        this.employeePayment = employeePayment;
    }

    public Employee getEmployeeByID(string id){
        return employees.FirstOrDefault(e => e.getID() == id);
    }
}
