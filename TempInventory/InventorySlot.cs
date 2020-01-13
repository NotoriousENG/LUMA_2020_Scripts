using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */
[System.Serializable]
public class InventorySlot : MonoBehaviour {

	BuildingObject building;	// Current building in the slot

	// Add building to the slot
	public void AddBuilding (BuildingObject newBuilding)
	{
		building = newBuilding;

	}

	// Clear the slot
	public void ClearSlot ()
	{
        building = null;
	}

    // If the remove button is pressed, this function will be called.
    public void RemoveBuildingFromInventory()
    {
        Inventory.instance.Remove(building);
    }

}
