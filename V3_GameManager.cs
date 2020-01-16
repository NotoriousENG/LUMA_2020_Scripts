using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_GameManager : MonoBehaviour
{
    /* =========================================================
     * MISSING PARTS THAT NEED TO BE IMPLEMENTED BY ORDER OF IMPORTANCE
     * =========================================================
     * 1. Condition Checking needs to be implemented
     * 2. Camera motion handling
     * 3. Responses to succeed/fail of building placement
     * 4. Might want to make a separate UI handling script that is called to handle all UI interactions and events
     * 5. Twitter functionality should be good by needs to be looked over
     */
    
    private GameObject currLvl;


    private class LevelVariables
    {
        public Color originalColor;
    }
    private LevelVariables lvlVars;
    public Color highlightColor;

    [Tooltip("Used for twitter functionality")]
    public Screencapture snapCam;      //Use snapCam.CallTakeSnapshot() to use the snapshot feature


    // Start is called before the first frame update
    void Start()
    {
        moveToLevel(this.GetComponent<LevelSelect>().firstLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Highlight current plot/level
    public void highlightLevel(GameObject level)
    {
        lvlVars.originalColor = level.GetComponent<Renderer>().material.color;
        level.GetComponent<Renderer>().material.color = highlightColor;
    }

    public void unhighlightLevel(GameObject level)
    {
        level.GetComponent<Renderer>().material.color = lvlVars.originalColor;
    }

    //Switches highlight to the next level
    public void moveToLevel(GameObject level)
    {
        currLvl = level;
        highlightLevel(currLvl);
    }

}
