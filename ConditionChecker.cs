using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    // Check if we are allowed to build a certain building on a certain plot
    public bool check(GameObject building, GameObject plot)
    {
        ZoningParams buildingParams =  building.GetComponent<ZoningParams>();
        ZoningParams plotParams = plot.GetComponent<ZoningParams>();

        List<ObjName> adjacentBuildings = plot.transform.parent.GetComponent<AdjacencyTracker>().objNames;
        ObjName buildingName = buildingParams.objName;

        return checkAdj(adjacentBuildings, buildingName) && checkParams(buildingParams, plotParams);
    }

    // check if we are not allowed to place a building next to anoother because of adjacency reasons
    private bool checkAdj(List<ObjName> adjacentBuildings, ObjName buildingName)
    {
        foreach (ObjName bld in adjacentBuildings)
        {
            if (bld.Equals(buildingName))
            {
                return false;
            }
        }
        return true;
    }

    // check other parameters (Zone Type, Height, if it is just illegal in this municipality)
    private bool checkParams(ZoningParams buildingParams, ZoningParams plotParams)
    {
        return checkAllowed(buildingParams) && checkHeightRestriction(buildingParams, plotParams) 
                && checkZoneType(buildingParams, plotParams);
    }

    // check if the building is ever allowed to be built
    private bool checkAllowed(ZoningParams buildingParams)
    {
        return !buildingParams.conditions.notAllowed;
    }

    // check if the building is acceptable to build based on height
    private bool checkHeightRestriction(ZoningParams buildingParams, ZoningParams plotParams)
    {
        // TODO: figure out what is needed to implement height restrictions per spec.
        // return buildingParams.conditions.heightRestriction.Equals(plotParams.conditions.heightRestriction);
        return true;
    }

    // check if there is an agreement between permitted types of buildings (zoning rules) and the building we want to build
    private bool checkZoneType(ZoningParams buildingParams, ZoningParams plotParams)
    {
        foreach (ZoneType plotZone in plotParams.zoneTypes)
        {
            foreach (ZoneType bldgZone in buildingParams.zoneTypes)
            {
                if (plotZone.Equals(bldgZone))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

