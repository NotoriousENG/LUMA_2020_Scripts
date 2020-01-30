using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2_GameController : MonoBehaviour
{

    [System.Serializable]
    public class CanvasElements
    {
        public GameObject responseScreen;
        public Text wrongOrRightText;
        public Text explanationText;
    }
    public CanvasElements canvasElements;

    [System.Serializable]
    public class PlotSelection
    {
        public GameObject targetPlot = null;
        public bool nextSelect = false;
    }
    public PlotSelection plotSelection;
    public GameObject zonePrefab;
    public SelectPlot selectPlot;
    public List<GameObject> sleepingPlots;

    public float zoomOutAmount;

    public Screencapture snapCam;

    public CameraFollowRotate cameraFollowRotate;

    //Lets the script know if the recent building placed was built or not. Built if building passed conditions
    private bool placementCorrect = false;
    private Responses responses; 


    // Start is called before the first frame update
    void Start()
    {
        responses = this.gameObject.GetComponent<Responses>();
        canvasElements.responseScreen.SetActive(false);
        foreach (Transform child in selectPlot.gameObject.transform)
        {
            if (child.name.Contains("Zone") && !child.gameObject.activeSelf)
            {
                sleepingPlots.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TEMPORARY FUNCTIONALITY USED TO TEST UI ELEMENTS
        if (Input.GetKeyDown(KeyCode.T))
        {
            canvasElements.responseScreen.SetActive(true);
        }

        //TESTING FOR SCREENSHOTS
        if (Input.GetKeyDown(KeyCode.P))
        {
            snapCam.CallTakeSnapshot();
        }
    }

    public void V2_BuildBuilding(GameObject building)
    {
        GameObject plot = plotSelection.targetPlot;
        if (checkConditions(plot.GetComponent<PlotConditions>(), building.GetComponent<BuildingConditions>())) 
        {
            GameObject newBuilding = Instantiate(building, plotSelection.targetPlot.transform.position, Quaternion.identity);
            newBuilding.transform.parent = plotSelection.targetPlot.transform; // parent to plot object

            placementCorrect = true;           
        }

        //Loads in reply if building can be placed or not
        if (placementCorrect)
        {
            canvasElements.wrongOrRightText.text = "That's Correct!";
        }
        else
        {
            canvasElements.wrongOrRightText.text = "Not quite...";
        }

        //Loads in the appropriate response for the building
        canvasElements.explanationText.text = responses.getExplanation(plotSelection.targetPlot.GetComponent<PlotConditions>().levelNum, 
            building.GetComponent<BuildingConditions>().conditions.BuildingName);

        toggleResponseScreen();
        
    }

    //Loads in the text that will be displayed on the popup screen


    //Builds a building on the current targetted plot. Switches nextSelect to true so that the next plot is selected
    public void BuildBuilding(GameObject buildingObject)
    {
        
        GameObject building = buildingObject.GetComponent<BuildingObject>().buildingObject;
        Transform zoneTransform = plotSelection.targetPlot.transform.parent.transform;
        ZoneInfo targetInfo = plotSelection.targetPlot.transform.parent.gameObject.GetComponent<ZoneInfo>();

        if (targetInfo.isFull)
        {
            return;
        }

        ZoneType buildingType =  building.GetComponent<ZoneInfo>().zoneType;
        ZoneType targetType = targetInfo.zoneType;
        //Make sure that the target plot is not null, then instantiate
        if (plotSelection.targetPlot != null && (buildingType == targetType || targetType == ZoneType.Wild))
        {
            if (targetType == ZoneType.Wild)
            {
                plotSelection.targetPlot.transform.parent.gameObject.GetComponent<ZoneInfo>().zoneType = buildingType;
            }
            GameObject newBuilding = Instantiate(building, plotSelection.targetPlot.transform.position, Quaternion.identity);
            newBuilding.transform.parent = plotSelection.targetPlot.transform; // parent to plot object
            targetInfo.usedPlots ++; // increment used count
            if (targetInfo.usedPlots == targetInfo.PlotCount)
            {
                //CameraZoom.CameraZoomOut(this,zoomOutAmount);
                targetInfo.isFull = true;

                GameObject newObj = sleepingPlots[Random.Range(0, sleepingPlots.Count)]; // get random zone
                newObj.SetActive(true);
                cameraFollowRotate.ZoomOut();
                cameraFollowRotate.objTarget = newObj.transform;
                // CameraZoom.CameraMoveToPlot(this, newObj);
                sleepingPlots.Remove(newObj);
                // GameObject newObj = GameObject.Instantiate(zonePrefab, zoneTransform.position, zoneTransform.rotation, zoneTransform.parent);
                // newObj.transform.position += new Vector3 (3,0,0);
                selectPlot.getNewPlots(newObj.transform);
            }
            /*newBuilding.transform.position = new Vector3(newBuilding.transform.position.x,
                newBuilding.transform.position.y + (newBuilding.transform.localScale.y * 0.5f) + (plotSelection.targetPlot.transform.localScale.y * 0.5f),
                newBuilding.transform.position.z);*/
            plotSelection.nextSelect = true;
        }
        else 
        {
            //toggleWrongZoneTypeScreen();
        }

    }

    /*Used to toggle on or off the UI Screen that pops up when the user attempts to place a building in a zone that doesnt match the 
     * current plot type. 
    */
    public void toggleResponseScreen()
    {
        if (canvasElements.responseScreen.activeInHierarchy == true)
        {
            if (placementCorrect)
            {
                plotSelection.nextSelect = true;
                placementCorrect = false;
            }
            canvasElements.responseScreen.SetActive(false);
        }
        else
        {
            canvasElements.responseScreen.SetActive(true);
        }
    }

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
