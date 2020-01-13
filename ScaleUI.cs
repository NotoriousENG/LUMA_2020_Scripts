using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    public Transform Center;
    public float maxScale, minScale;
    public float maxDistance = 960;

    public float getScale(Transform obj)
    {
        float scale = (maxDistance - obj.position.x)/maxDistance;
        if (scale < minScale)
        {
            scale = minScale;
        }
        return scale;
    }
    public void applyScale(Transform obj)
    {
        float scale = getScale(obj);
        obj.localScale = Vector3.one * scale;
    }
}
