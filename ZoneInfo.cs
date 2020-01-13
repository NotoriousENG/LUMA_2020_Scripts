using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoneType
{
    Wild, Residential, Industrial, Commercial, Environmental
}
public class ZoneInfo : MonoBehaviour
{
    public ZoneType zoneType = ZoneType.Wild;
    [HideInInspector]
    public int PlotCount;
    public int usedPlots;
    public bool isFull;

    private void Start() 
    {
        setPlotCount();
    }

    private void setPlotCount()
    {
        PlotCount =  transform.childCount;
    }
}
