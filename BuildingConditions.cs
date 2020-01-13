using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConditions : MonoBehaviour
{
    [System.Serializable]
    public class Conditions
    {
        public string BuildingName;
        //Zone types this building is part of
        public string[] ZoneType;
        //Buildings this building cannot be placed next to
        public string[] CannotBeNextToBuilding;
        //Zones this building cannot be placed next to
        public string[] CannotBeNextToZone;
        //Does the building have a height restriction
        public bool heightRestriction = false;
        //Is the building ever allowed to be placed
        public bool notAllowedAtAll = false;

    }
    public Conditions conditions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

}
