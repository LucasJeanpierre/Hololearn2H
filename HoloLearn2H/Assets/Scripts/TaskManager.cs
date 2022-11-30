

using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TaskManager : Singleton<TaskManager>
{

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void GenerateObjectsInWorld();

    public abstract void DestroyObjects();

    public virtual GameObject GetClosestObject()
    {
        Rigidbody[] remainingObjects = GameObject.FindGameObjectWithTag("ObjectsToBePlaced").GetComponentsInChildren<Rigidbody>();
        List<GameObject> targets = new List<GameObject>();
        foreach (Rigidbody target in remainingObjects)
        {
            if (target.gameObject.GetComponent<ManipulationHandler>().enabled == true)
            {
                targets.Add(target.gameObject);
            }
        }

        SortByDistance(targets);

        return targets[0];
    }

    public virtual GameObject GetClosestTarget()
    {
        GameObject draggedObject = VirtualAssistantManager.Instance.targetObject.gameObject;
        Debug.Log(draggedObject);
        string tag = draggedObject.tag;

        Rigidbody[] placements = GameObject.FindGameObjectWithTag("Targets").GetComponentsInChildren<Rigidbody>();
        List<GameObject> targets = new List<GameObject>();
        foreach (Rigidbody target in placements)
        {
            if (target.gameObject.tag == tag)
            {
                targets.Add(target.gameObject);
            }
        }

        SortByDistance(targets);
        return targets[0];
    }


    protected void SortByDistance(List<GameObject> targets)
    {
        GameObject temp;
        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = 0; j < targets.Count - 1; j++)
            {
                if (Vector3.Distance(targets.ElementAt(j).transform.position, VirtualAssistantManager.Instance.transform.position)
                    > Vector3.Distance(targets.ElementAt(j + 1).transform.position, VirtualAssistantManager.Instance.transform.position))
                {
                    temp = targets[j + 1];
                    targets[j + 1] = targets[j];
                    targets[j] = temp;
                }
            }
        }
    }


    /// <summary>
    /// Adjusts the initial position of the object if it is being occluded by the spatial map.
    /// </summary>
    /// <param name="position">Position of object to adjust.</param>
    /// <param name="surfaceNormal">Normal of surface that the object is positioned against.</param>
    /// <returns></returns>
    protected virtual Vector3 AdjustPositionWithSpatialMap(Vector3 position, Vector3 surfaceNormal)
    {
        Vector3 newPosition = position;
        RaycastHit hitInfo;
        float distance = 0.5f;

        // Check to see if there is a SpatialMapping mesh occluding the object at its current position.
        if (Physics.Raycast(position, surfaceNormal, out hitInfo, distance, SpatialMappingManager.Instance.LayerMask))
        {
            // If the object is occluded, reset its position.
            newPosition = hitInfo.point;
        }

        return newPosition;
    }

}
