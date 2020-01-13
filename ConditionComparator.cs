using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionComparator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns true if building is allowed to be placed, false if it is not allowed
    //Takes in the stated scripts from gameobjects
    public bool checkConditions(PlotConditions plotCon, BuildingConditions buildCon)
    {
        //If building isn't allowed to be built at all, return false
        if (buildCon.conditions.notAllowedAtAll)
        {
            return false;
        }
        //Returns false if the building has a height restriction and the plot doesn't allow height restricted buildings
        if (buildCon.conditions.heightRestriction && !plotCon.conditions.allowHeightRestrictedBuilding)
        {
            return false;
        }
        //Makes sure that the zone types match
        bool typeMatch = false;
        for (int i = 0; i < buildCon.conditions.ZoneType.Length; i++)
        {
            if (buildCon.conditions.ZoneType[i] == plotCon.conditions.ZoneType)
            {
                typeMatch = true;
            }
        }
        if (!typeMatch)
        {
            return false;
        }
        //Makes sure that there are no zones nearby that disallow a building to be built
        for (int i = 0; i < buildCon.conditions.CannotBeNextToZone.Length; i++)
        {
            for (int j = 0; j < plotCon.conditions.nearbyZones.Length; j++)
            {
                if (plotCon.conditions.nearbyZones[j] == buildCon.conditions.CannotBeNextToZone[i])
                {
                    return false;
                }
            }
        }
        //Makes sure that there are no buildings nearby that disallow a building to be built
        for (int i = 0; i < buildCon.conditions.CannotBeNextToBuilding.Length; i++)
        {
            for (int j = 0; j < plotCon.conditions.nearbyBuildings.Length; j++)
            {
                if (plotCon.conditions.nearbyBuildings[j] == buildCon.conditions.CannotBeNextToBuilding[i])
                {
                    return false;
                }
            }
        }
        //Makes sure that the building is not disallowed by the zone
        for (int i = 0; i < plotCon.conditions.unallowedBuildings.Length; i++)
        {
            if (plotCon.conditions.unallowedBuildings[i] == buildCon.conditions.BuildingName)
            {
                return false;
            }
        }

        return true;
    }
}
