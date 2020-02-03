using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotConditions : MonoBehaviour
{
    public int levelNum;
    [System.Serializable]
    public class Plotconditions
    {
        //Zone type of the plot
        public string ZoneType;
        //What zones are nearby?
        public string[] nearbyZones;
        //What buildings are nearby?
        public string[] nearbyBuildings;
        //What buildings are not allowed at all?
        public string[] unallowedBuildings;
        //Can the building have a height restriction?
        public bool allowHeightRestrictedBuilding = false;

    }
    public Plotconditions conditions;

    private void Start()
    {
        
    }
}
