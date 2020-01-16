using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [Tooltip("Enter levels (GameObject plots) in order of game flow")]
    public GameObject[] level;
    private int levelIndex;

    /*===========================================================
     * To Do With This Script and It's Implementation
     * 1. In the game manager, have functions that highlight the plot with another color
     * 2. Have the game manager save the current plot and enter it into a camera script that will "Look At" the current plot
     * ===========================================================
     */

    // This function is to be called in the Game Manager Start Function 
    public GameObject firstLevel()
    {
        //Debug.Log("Initialized Level " + (levelIndex + 1));
        return level[levelIndex];
    }


    //This function is to be called after a successfull building placement
    public GameObject nextLevel()
    {
        levelIndex++;
        //Debug.Log("Level " + (levelIndex) + " finished.");
        if (levelIndex >= level.Length)
        {
            Debug.Log("All Levels Complete");
            return null;
        }
        //Debug.Log("Level " + (levelIndex + 1) + " started.");
        return level[levelIndex];
    }
}
