using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementsDisabled : PlacementsManager {
   

    // Use this for initialization
    public override void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public override void MakeLevelPrefabDisappear(GameObject draggedObject)
    {
        Rigidbody[] disappearingObjects = GameObject.Find("TableMates").GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody rb in disappearingObjects)
        {
            if(draggedObject.tag != rb.tag)
            {
                rb.gameObject.SetActive(false);
            }
        }
    }

    public override void MakeLevelPrefabReapper()
    {
        Rigidbody[] remainedObjects = GameObject.Find("TableMates").GetComponentsInChildren<Rigidbody>(true);
        //Debug.Log(remainedObjects.Length);

        foreach (Rigidbody rb in remainedObjects)
        {
            rb.gameObject.SetActive(true);
        }

    }
}
