using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion//Means only of these can occur per scene ie only one inventory

	public delegate void OnBuildingChanged();
	public OnBuildingChanged onBuildingChangedCallback;

	public int space = 10;	// Amount of building spaces

	// Our current list of items in the inventory
	public List<BuildingObject> buildings = new List<BuildingObject>();

	// Add a new building if enough room
    //TODO ADD
	public void Add (BuildingObject building)
	{
			buildings.Add (building);

			if (onBuildingChangedCallback != null)
				onBuildingChangedCallback.Invoke ();
	}

	// Remove an building
	public void Remove (BuildingObject item)
	{
		buildings.Remove(item);

		if (onBuildingChangedCallback != null)
			onBuildingChangedCallback.Invoke();
	}

}
