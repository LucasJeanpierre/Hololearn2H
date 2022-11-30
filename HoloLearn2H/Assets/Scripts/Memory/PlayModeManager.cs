using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayModeManager : MonoBehaviour
{
    public bool IsBusy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract List<Transform> GenerateObjects(GameObject ObjectsPrefabs, int numberOfBoxes);

    public abstract void StartGame(int waitingTime);

    public abstract void HandleTap(Transform parent);
}
