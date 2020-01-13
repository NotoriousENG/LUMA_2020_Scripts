using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClickObject : MonoBehaviour
{
    public Color clickColor, hoverColor;
    private Color originalColor;

    private void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }
    private void OnMouseEnter()
    {
        Debug.Log(gameObject.name);
        ChangeColor(hoverColor);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor(clickColor);
        }
    }

    private void OnMouseExit()
    {
        ChangeColor(originalColor);
    }


    private void ChangeColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

}
