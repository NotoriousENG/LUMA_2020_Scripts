﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from tutorial: https://www.youtube.com/watch?v=hRRqxrWQJQg
public class CameraFollowRotate : MonoBehaviour {
    public Transform target;
    public Transform objTarget;
    private Transform originalTarget;
    public float zoomOutAmmount = .5f;
    public float moveSpeed = 1;
    public float zoomSpeed = 1;
    public Vector3 offsetPosition;
    private Vector3 nextOffsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;
    
    private void Start() 
    {
        nextOffsetPosition = offsetPosition;
        originalTarget = objTarget;
    }
    private void LateUpdate()
    {
        Refresh();
    }

    public void ZoomOut()
    {
        nextOffsetPosition = offsetPosition + new Vector3 (-1,1,0) * zoomOutAmmount;
    }

    public void Interpolate()
    {
        if (objTarget.position != target.position)
        {
            
            target.position = Vector3.MoveTowards(target.transform.position, (objTarget.transform.position - originalTarget.transform.position)/2, moveSpeed * Time.deltaTime);
        }
        if (nextOffsetPosition != offsetPosition)
        {
            offsetPosition = Vector3.MoveTowards(offsetPosition, nextOffsetPosition, zoomSpeed * Time.deltaTime);
        }
        
    }
    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        Interpolate();

        // compute rotation
        if (lookAt)
        {
            if (target.position == objTarget.position)
            {
                transform.LookAt(target);
            }
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}