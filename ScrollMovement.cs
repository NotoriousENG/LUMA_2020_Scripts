using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollMovement : ScaleUI
{
    public ScrollRect scrollRect;
    public Transform leftBoundary;
    public Transform rightBoundary;
    public Transform leftEdge;
    public Transform rightEdge;
    public Transform content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftBoundary.position.x+75 <= leftEdge.position.x)
        {
            scrollRect.StopMovement();
            Debug.Log("Passed left boundary "+ leftBoundary.position.x +" "+leftEdge.position.x);
            content.position = new Vector3(leftEdge.position.x+125, content.position.y, 0);
            //Todo
            ScaleEverything();
        }
        
        if(rightBoundary.position.x-75 >= rightEdge.position.x)
        {
            scrollRect.StopMovement();
            content.position = new Vector3(rightEdge.position.x-125, content.position.y, 0);
            Debug.Log("Passed right boundary");
            ScaleEverything();
        }
    }

    private void ScaleEverything()
    {
        foreach (Transform child in content)
        {
            applyScale(child);
        }
    }
}
