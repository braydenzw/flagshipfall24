/*
* This class should allow the player to manage their staff members.
*
* This means all roster management:
*  1. Firing current employees
*  2. Hiring new employees from available list
*  3. Managing shifts (who is assigned to what station. payment)
*/

public enum Station {
    Toss, Top, Ovens, Cut, Orders
}

public class StaffManager {
    private StaffData staffData;

    public StaffManager(StaffData staffData){
        this.staffData = staffData;

        if(staffData == null){
            this.staffData = new StaffData();
        }
    }

    public StaffData getStaffData() { return staffData; }

    // TODO: some function to change station assignment
    // TODO: some function to fire employee
    // TODO: some function to hire employee
    // TODO: some function to update payment
}
