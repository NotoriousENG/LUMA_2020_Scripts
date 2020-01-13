using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [System.Serializable]
    public class CanvasElements
    {
        public GameObject wrongZoneTypeScreen;
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

    
    // Start is called before the first frame update
    void Start()
    {
        canvasElements.wrongZoneTypeScreen.SetActive(false);
        foreach(Transform child in selectPlot.gameObject.transform)
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
            canvasElements.wrongZoneTypeScreen.SetActive(true);
        }

        //TESTING FOR SCREENSHOTS
        if (Input.GetKeyDown(KeyCode.P))
        {
            snapCam.CallTakeSnapshot();
        }
    }

    
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
            toggleWrongZoneTypeScreen();
        }

    }

    /*Used to toggle on or off the UI Screen that pops up when the user attempts to place a building in a zone that doesnt match the 
     * current plot type. 
    */
    public void toggleWrongZoneTypeScreen()
    {
        if (canvasElements.wrongZoneTypeScreen.activeInHierarchy == true)
        {
            canvasElements.wrongZoneTypeScreen.SetActive(false);
        }
        else
        {
            canvasElements.wrongZoneTypeScreen.SetActive(true);
        }
    }
}
