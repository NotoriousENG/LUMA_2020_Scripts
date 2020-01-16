using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attatch to the parrent of plots 
 * (each should have a group of buildings that are adjacent)
 * i.e. for 4 adjacent plots:
 * Design0
 *  |->Bldg0
 *  |->Bldg1
 *  |->Bldg2
 *  |->Level Plot (The one the user is allowed to build on)
 */

public class AdjacencyTracker : MonoBehaviour
{
    public List<ObjName> objNames;
    // [HideInInspector]
    // public List<ZoningParams> zoningParams;

    private void Start()
    {
        GetAdjacency();
    }

    // Stores each object in a list
    public void GetAdjacency()
    {
        // zoningParams.Clear();
        objNames.Clear();
        
        foreach (Transform child in transform)
        {
            var tmp = child.GetComponent<ZoningParams>();
            // zoningParams.Add(tmp);
            objNames.Add(tmp.objName);
        }
    }
}
