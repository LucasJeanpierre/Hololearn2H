

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.IO;

//using Microsoft.MixedReality.Toolkit;

public class LayTheTableManager : TaskManager
{
    public GameObject LevelsPrefabs;
    public GameObject ObjectsPrefabs;
    public GameObject VirtualAssistantsPrefabs;
    public GameObject PlacementsManagerPrefabs;

    private int numberOfLevel;
    private int numberOfPeople;
    private int targetsVisibility;
    private int assistantPresence;
    private int selectedAssistant;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;
    private Transform selectedLevel;

    // Use this for initialization
    public override void Start() {
        LoadSettings();

        selectedLevel = LevelsPrefabs.transform.GetChild(numberOfLevel-1);
        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant + 1).GetChild(assistantBehaviour-1);

        Instantiate(PlacementsManagerPrefabs.transform.GetChild(targetsVisibility), GameObject.Find("LayTheTableManager").transform);
    }

    // Update is called once per frame
    public override void Update()
    {
    
    }



    public override void GenerateObjectsInWorld()
    {
        //Seleziono il tavolo dove guarda l'utente
        // Transform table = TableSelect(SpatialProcessing.Instance.tables);

        // Bounds tableColliderBounds = table.GetColliderBounds();

        // Vector3 tableEdge1 = table.TransformPoint(0.4f, 0f, 0f);
        // Vector3 tableEdge2 = table.TransformPoint(-0.4f, 0f, 0f);
        // Vector3 tableEdge3 = table.TransformPoint(0f, -0.4f, 0f);
        // Vector3 tableEdge4 = table.TransformPoint(0f, 0.4f, 0f);


        // List<Vector3> tableEdges = new List<Vector3>() { tableEdge1, tableEdge2, tableEdge3, tableEdge4 };
        // Debug.DrawLine(tableEdge1, tableColliderBounds.center, Color.black, 30f);
        // Debug.DrawLine(tableEdge2, tableColliderBounds.center, Color.black, 30f);
        // Debug.DrawLine(tableEdge3, tableColliderBounds.center, Color.red, 30f);
        // Debug.DrawLine(tableEdge4, tableColliderBounds.center, Color.red, 30f);

        // List<Quaternion> rotations = new List<Quaternion>();

        // for (int i=0; i<tableEdges.Count; i++)
        // {
        //     Vector3 relativeDirection = tableColliderBounds.center - tableEdges.ElementAt(i);
        //     Quaternion rotation = Quaternion.LookRotation(relativeDirection);
        //     rotations.Add(rotation);
        // }


        // Transform objectsToBePlaced = selectedLevel.gameObject.GetComponent<ObjectsGenerator>().GenerateObjects(ObjectsPrefabs.transform, numberOfPeople);
        // objectsToBePlaced.Translate(tableEdge1);
        // objectsToBePlaced.Rotate(rotations.ElementAt(0).eulerAngles);


        // Transform sceneRoot = GameObject.Find("Broadcasted Content").transform;

        // Transform tablePlacements = new GameObject("TableMates").transform;
        // tablePlacements.parent = sceneRoot;
        // tablePlacements.tag = "Targets";

        // Transform tableMatesPlacements = selectedLevel.Find("TableMatePlacement");
        // for (int i=1; i<=numberOfPeople; i++)
        // {
        //     Instantiate(tableMatesPlacements.gameObject, tableEdges.ElementAt(i) + new Vector3(0f, 0.01f, 0f), rotations.ElementAt(i), tablePlacements);
        // }

        // Transform beveragesPlacements = selectedLevel.Find("BeveragesPlacement");
        // Instantiate(beveragesPlacements.gameObject, tableColliderBounds.center + new Vector3(0f, 0.01f, 0f), beveragesPlacements.transform.rotation, tablePlacements);

        // Counter.Instance.InitializeCounter(objectsToBePlaced.GetComponentsInChildren<Rigidbody>().Length);



        // Vector3 assistantPosition = table.TransformPoint(-0.2f, 0f, 0f);

        // if (assistantPresence != 0)
        // {
        //     Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation, sceneRoot);
        //     VirtualAssistantManager.Instance.patience = assistantPatience;
        // }
    }

    // private Transform TableSelect(List<GameObject> tables)
    // {
        // Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        // RaycastHit hitInfo;
        // if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        // {
        //     gazePosition = hitInfo.point;
        // }

        // float minDistance = 1000f;
        // Transform nearestTable = null;
        // foreach (GameObject table in tables)
        // {
        //     Vector3 tableCenter = table.transform.GetColliderBounds().center;
        //     if (Vector3.Distance(tableCenter, gazePosition) <= minDistance)
        //     {
        //         minDistance = Vector3.Distance(tableCenter, gazePosition);
        //         nearestTable = table.transform;
        //     }
        // }

        // foreach (GameObject table in tables)
        // {
        //     if (table.GetInstanceID() != nearestTable.gameObject.GetInstanceID())
        //     {
        //         Destroy(table.gameObject);
        //     }
        // }

        // return nearestTable;
    //}
    

    private void LoadSettings()
    {
        numberOfPeople = LayTheTableSettings.Instance.numberOfPeople;
        numberOfLevel = LayTheTableSettings.Instance.numberOfLevel;
        targetsVisibility = LayTheTableSettings.Instance.targetsVisibility;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }


    public override void DestroyObjects()
    {
        if (VirtualAssistantManager.Instance != null)
        {
            Destroy(VirtualAssistantManager.Instance.gameObject);
        }
        Destroy(GameObject.Find("ObjectsToBePlaced"));
        Destroy(GameObject.Find("TableMates"));
    }
}
