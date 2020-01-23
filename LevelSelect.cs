using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [Tooltip("Enter levels (GameObject plots) in order of game flow")]
    public GameObject[] levelArr;
    public Queue<GameObject> levels;
    public Color highlightColor;
    private Color originalColor;

    /*===========================================================
     * To Do With This Script and It's Implementation
     * 1. In the game manager, have functions that highlight the plot with another color
     * 2. Have the game manager save the current plot and enter it into a camera script that will "Look At" the current plot
     * ===========================================================
     */

     private void Start() 
     {
        levels = new Queue<GameObject>();
         foreach (GameObject obj in levelArr)
         {
             levels.Enqueue(obj);
         }
     }


    // return the current element
    public GameObject currLevel()
    {
        return levels.Peek(); 
    }


    //This function is to be called after a successfull building placement 
    // also handles highlighting plots
    public GameObject getNextLevel()
    {
        unhighlightLevel(levels.Dequeue());
        highlightLevel(currLevel());
        return currLevel();
    }

    public void highlightLevel(GameObject level)
    {
        originalColor =  level.GetComponent<Renderer>().material.color;
        level.GetComponent<Renderer>().material.color = highlightColor;
    }

    public void unhighlightLevel(GameObject level)
    {
        level.GetComponent<Renderer>().material.color = originalColor;
    }
}
