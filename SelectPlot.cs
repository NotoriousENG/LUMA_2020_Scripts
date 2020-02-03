using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlot : MonoBehaviour
{
    public Color selectedColor;
    private Color originalColor;
    public List<GameObject> plots;
    private int currentPlot = 0;
    private bool allPlotsFilled = false;
    
    private V2_GameController gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tmpPlots = GameObject.FindGameObjectsWithTag("Plot");
        foreach (GameObject plot in tmpPlots)
        {
            plots.Add(plot);
        }

        originalColor = plots[0].GetComponent<Renderer>().material.color;    
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<V2_GameController>();
        highlight(plots[currentPlot]);

    }

    // Update is called once per frame
    void Update()
    {
        //handles the state of the current plot that is selected
        if (gameManager.plotSelection.nextSelect && !allPlotsFilled)
        {
            selectNextPlot();
        }
    }

    void selectNextPlot()
    {
        if (currentPlot < plots.Count)
        {
            //Set current plot that was just built on to its original color
            plots[currentPlot].GetComponent<Renderer>().material.color = originalColor;
            //If on the last plot, change allPlotsFilled bool to true so this code statement will no longer be called
            if (currentPlot + 1 == plots.Count)
            {
                allPlotsFilled = true;
                // gameManager.plotSelection.targetPlot = null;
            }
            else
            {
                //Highlights the next plot to be built on and sets nextSelect to false until the gameManager builds the next building
                highlight(plots[++currentPlot]);
                gameManager.plotSelection.nextSelect = false;
            }

        }
    }

    //Enter in the game object that will change its color to indicate it is selected
    //Sets the gameManager to know which plot is currently being built on.
    void highlight(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = selectedColor;
        gameManager.plotSelection.targetPlot = obj;
    }

    public void getNewPlots(Transform zone)
    {
        List<GameObject> tmpPlots = new List<GameObject>();
        foreach (Transform child in zone)
        {
            plots.Add(child.gameObject);
        }
    }


}
