using UnityEngine;

/* The base building class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Building", menuName = "Inventory/Building")]//Makes it so that you can just make an object of this 
[System.Serializable]
public class Building : ScriptableObject {

	new public string name = "New Building";	// Name of the building
	public Sprite icon = null;				// Building icon
	public bool showInInventory = true;

	// Called when the building is pressed in the inventory
	public virtual void Use ()
	{
		// TODO: Add the building to the thing (Add link to the building manager here)
	}

	// Call this method to remove the building from inventory
	public void RemoveFromInventory ()
	{
		// Inventory.instance.Remove(this);
	}

}
