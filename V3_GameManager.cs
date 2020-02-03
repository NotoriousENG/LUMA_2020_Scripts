﻿using CameraSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_GameManager : MonoBehaviour
{
    public LevelSelect levelSelect;
    public ConditionChecker conditionChecker;
    /* =========================================================
     * MISSING PARTS THAT NEED TO BE IMPLEMENTED BY ORDER OF IMPORTANCE
     * =========================================================
     * 1. Condition Checking needs to be implemented
     * 2. Camera motion handling
     * 3. Responses to succeed/fail of building placement
     * 4. Might want to make a separate UI handling script that is called to handle all UI interactions and events
     * 5. Twitter functionality should be good by needs to be looked over
     */
    // private LevelVariables lvlVars;


    [Tooltip("Used for twitter functionality")]
    public Screencapture snapCam;      //Use snapCam.CallTakeSnapshot() to use the snapshot feature
    public CameraSystem_Camera cinCam;
    public SmartResponses responses;
    public M_UI_Manager ui_Manager;
    public bool defaultSpotQ = false;


    // Start is called before the first frame update
    void Start()
    {
        // moveToLevel(this.GetComponent<LevelSelect>().firstLevel());
        levelSelect.highlightLevel(levelSelect.currLevel());
        // moveToLevel(levelSelect.currLevel());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            snapCam.CallTakeSnapshot();
        }
    }

    public void build(GameObject building)
    {
        // if we can build the building, build it
        bool canBuild = conditionChecker.check(building, levelSelect.currLevel());
        if (canBuild)
        {
            // Debug.Log(building.name + " can be built");
            Instantiate(building, levelSelect.currLevel().transform.position, Quaternion.identity);
            // Debug.Log(building.name + " was built");
            // TODO: handle UI messages (success)
            // moveToLevel(); //DELETE ME LATER. FOR TESTING PURPOSES ONLY
            
            if (cinCam.camQ.Count != 0 && defaultSpotQ) //TODO: Remove when there are more camera spots than plots
            {
                switchCameraSpots();
            }
            // Invoke(moveToLevel)
        }
        else
        {
            // Debug.Log(building.name + " can't be built! Try again!");
            // TODO: handle UI messages (failure)
        }
        // Debug.Log("RESPONSE: ");
        var p = levelSelect.currLevel().GetComponent<ZoningParams>();
        string response = (responses.getResponse(building, p.zoneTypes, canBuild));

        // TODO: Print response to UI Element
        Debug.Log(response);
        ui_Manager.EditMessage(getHeader(canBuild), response, canBuild);        
    }

    //Switches highlight to the next level
    public void moveToLevel()
    {
        // TODO: handle camera
        if (levelSelect.levels.Count != 1)
        {
            levelSelect.getNextLevel();
            // Debug.Log("Moving to next plot");
        }
        else
        {
            Debug.Log("Queue is empty");
        }
        
    }

    public void switchCameraSpots()
    {
        if (cinCam.camQ.Count != 0)
            cinCam.SwitchCameraSpots(cinCam.camQ.Dequeue());
    }

    string getHeader(bool success)
    {
        if (success)
        {
            return "Success";
        }
        return "failure";
    }

}
