using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The zone categories used to describe objects
public enum ZoneTypes
    {
        General_Residential,
        Single_Family_House,
        Public_Services,
        Commercial,
        Light_Industrial,
        Education
    }

// The availiable names for buildings (Use Zone for empty plots)
public enum ObjName
{
    Zone,
    Yoga_Studio,
    Micro_Brewery,
    Single_Family_House,
    Two_Story_Duplex,
    Ten_Story_Apartment,
    Manufacturing_Facility,
    Historic_Building,
    Grocery_Store,
    Restaurant,
    Gas_Station,
    Bar,
    House_of_Worship,
    Local_Government_Building,
    Parking_Lot,
    Art_Gallery,
    School
}

// holds parameters for a building or an empty plot
// used by condition checker to see if the combination is allowed
public class ZoningParams : MonoBehaviour
{
    public ObjName objName;
    public ZoneTypes[] zoneTypes;
    
    [System.Serializable]
    public class Conditions
    {
        // array of objects this can not be adjacent to
        public ObjName[] cantBeAdjacent;
        // if this zone has a height restriction (true) or this building violates a height restriction (true)
        // set to false otherwise
        public bool heightRestriction;
        // if this building is not allowed to be build (outlawed by municipality)
        public bool notAllowed;
    }
    public Conditions conditions;
}