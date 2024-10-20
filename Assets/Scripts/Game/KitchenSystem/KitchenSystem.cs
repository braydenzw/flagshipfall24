/*
* This class is the main container of all aspects of the main game.
*
* This includes:
*  1. An instance of StaffManager
*  2. An instance of OrderManager
*  3. An instance of Kitchen
*
* This should be interactable 
*/

public class KitchenSystem {
    
    private StaffManager staffManager;
    private OrderManager orderManager;
    private Kitchen kitchen;

    public KitchenSystem(StaffData staffData) {
        staffManager = new StaffManager(staffData);
        orderManager = new OrderManager();
        kitchen = new Kitchen();
    }

    // TODO: Function to handle any order submissions 
        // This should involve connecting the OrderManager and Kitchen objects
}
