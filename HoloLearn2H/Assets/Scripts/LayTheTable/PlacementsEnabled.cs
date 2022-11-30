using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementsEnabled : PlacementsManager
{
    
    // Use this for initialization
    public override void Start () {
		
	}

    // Update is called once per frame
    public override void Update () {
		
	}

    public override void MakeLevelPrefabDisappear(GameObject draggedObject)
    {
        return;
    }

    public override void MakeLevelPrefabReapper()
    {
        return;
    }
}
