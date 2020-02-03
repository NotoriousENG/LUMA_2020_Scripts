using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartResponses : MonoBehaviour
{
    public List<string> rightRemarks = new List<string>() {"Excellent! ", "Yes! ", "You're correct! "};
    public List<string> wrongRemarks = new List<string>() {"Not Quite ", "Hmm. Try again. "};
    private string templateZone = "This area is zoned for ";
    private string templateFor = "This zone is for things like ";
    private string templateGreat = "would make a great addition to this neighborhood. ";
    private string templateBad = "is not allowed because that is for ";

    public string getResponse(GameObject building, ZoneTypes[] plotTypes, bool isCorrect)
    {
        var buildingParams = building.GetComponent<ZoningParams>();
        var buildingZones = buildingParams.zoneTypes;
        // (Excellent!/Not quite.) This area is zoned for General Residential
        var str = initRemark(isCorrect) + templateZone + printAllZones(plotTypes) + ". ";

        // ... Single Family Home would make a great addition to this neighborhood.
        // ... Single Family Home is not allowed because that is for ...
        str += "Your " +  buildingParams.objName.ToString().Replace("_"," ") + " " + finalResponse(isCorrect);

        if (!isCorrect)
        {
            str += printAllZones(buildingZones) + " areas.";
        }

        return str;
    }

    public string initRemark(bool isCorrect)
    {
        if (isCorrect)
        {
            return randomFromList(rightRemarks);
        }else
        {
            return randomFromList(wrongRemarks);
        }
    }

    public string finalResponse(bool isCorrect)
    {
        if (isCorrect)
        {
            return templateGreat;
        }
        else
        {
            return templateBad;
        }
    }

    public string randomFromList(List<string> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public string printZones(ZoneTypes[] z0, ZoneTypes[] z1, bool isCorrect)
    {
        if (isCorrect)
        {
            return printAllZones(z0);
        }
        else
        {
            return printCorrectZones(z0, z1);
        }
    }

    public string printCorrectZones(ZoneTypes[] zoneTypes0, ZoneTypes[] zoneTypes1)
    {
        string s = null;
        foreach (var z0 in zoneTypes1)
        {
            foreach (var z1 in zoneTypes1)
            {
                if (z0 == z1)
                {
                    s += z0.ToString() + "/ ";
                }
            }
        }
        s = s.Remove(s.Length - 2);
        return s;
    }
    public string printAllZones(ZoneTypes[] zoneTypes)
    {
        string s = null;

        foreach (var z in zoneTypes)
        {
            s += z.ToString() + "/ ";
        }
        s = s.Remove(s.Length - 2).Replace("_", " ");
        return s;
    }
    public string fromZoneType(ZoneTypes zoneType)
    {
        return zoneType.ToString();
    }
}
