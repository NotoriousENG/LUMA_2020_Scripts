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

    public string getResponse(GameObject building, ZoneType plotType, bool isCorrect)
    {
        // (Excellent!/Not quite.) This area is zoned for General Residential
        var str = initRemark(isCorrect) + templateZone + fromZoneType(plotType) + ". ";
        // ... Single Family Home would make a great addition to this neighborhood.
        // ... Single Family Home is not allowed because that is for ...
        str +=  building.name + " " + finalResponse(isCorrect);
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

    public string fromZoneType(ZoneType zoneType)
    {
        return zoneType.ToString();
    }
}
