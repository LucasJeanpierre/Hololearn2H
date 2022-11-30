
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectsGenerator : Singleton<ObjectsGenerator> {

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract Transform GenerateObjects(Transform objectsPrefab, int numberOfPeople);
}
