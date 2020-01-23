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


    // Start is called before the first frame update
    void Start()
    {
        // moveToLevel(this.GetComponent<LevelSelect>().firstLevel());
        levelSelect.highlightLevel(levelSelect.currLevel());
        // moveToLevel(levelSelect.currLevel());
    }

    public void build(GameObject building)
    {
        // if we can build the building, build it
        if (conditionChecker.check(building, levelSelect.currLevel()))
        {
            Instantiate(building, levelSelect.currLevel().transform);
            // TODO: handle UI messages (success)
            // Invoke(moveToLevel)
        } else
        {
            // TODO: handle UI messages (failure)
        }
    }

    //Switches highlight to the next level
    public void moveToLevel()
    {
        // TODO: handle camera
        levelSelect.getNextLevel();
    }

}
