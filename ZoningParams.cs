using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZoneTypes
    {
        General_Residential,
        Single_Family_House,
        Public_Services,
        Commercial,
        Light_Industrial,
        Education
    }

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
    Art_Gallery
}
public class ZoningParams : MonoBehaviour
{
    public ObjName objName;
    public ZoneTypes[] zoneTypes;
    
    [System.Serializable]
    public class Conditions
    {
        public ObjName[] cantBeAdjacent;
        public bool heightRestriction;
        public bool notAllowed;
    }
    public Conditions conditions;
}