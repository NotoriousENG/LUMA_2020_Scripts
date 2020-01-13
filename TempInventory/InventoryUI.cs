using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* This object manages the inventory UI. */
[System.Serializable]
public class InventoryUI : MonoBehaviour {

	public GameObject inventoryUI;	// The entire UI of the inventory 
	public Transform buildingsParent;	// The parent object of all the buildings

	Inventory inventory;	// Our current inventory

/* 	void Start ()
	{
		inventory = Inventory.instance;
		inventory.onBuildingChangedCallback += UpdateUI;
	} */

	// Check to see if we should open/close the inventory
	void Update ()
	{
		/*if (Input.GetButtonDown("Inventory"))//IE TODO: Make this touch or on click which 
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			UpdateUI();
		}
        */
	}

	// Update the inventory UI by:
	//		- Adding Buidlingss
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	/* public void UpdateUI ()
	{
		InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();//Each child has an inventory slot component which is the holder

		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.buildings.Count)
			{
				slots[i].AddBuilding(inventory.buildings[i]);
			} else
			{
				slots[i].ClearSlot();
			}
		}
	} */

}
