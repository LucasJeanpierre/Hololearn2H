


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TutorialManager : TaskManager
{

    public GameObject Targets;
    public GameObject ObjectsToBePlaced;
    public GameObject VirtualAssistantsPrefabs;

    private int assistantPresence;
    private int selectedAssistant;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;


    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant + 1).GetChild(assistantBehaviour - 1);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld()
    {
        // Transform floor = SpatialProcessing.Instance.floors.ElementAt(0).transform;
        // SurfacePlane plane = floor.GetComponent<SurfacePlane>();
        

        // Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        // floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);

        // Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        // RaycastHit hitInfo;
        // if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        // {
        //     gazePosition = hitInfo.point;
        // }

        // Vector3 objsPosition = gazePosition;
        // objsPosition.y = floorPosition.y;


        // Vector3 relativePos = Camera.main.transform.position - gazePosition;
        // Quaternion rotation = Quaternion.LookRotation(relativePos);
        // rotation.x = 0f;
        // rotation.z = 0f;


        // Transform sceneRoot = GameObject.Find("Broadcasted Content").transform;

        // Transform targets = new GameObject("Targets").transform;
        // targets.parent = sceneRoot;
        // targets.tag = "Targets";
        // Instantiate(Targets.transform.GetChild(0), Targets.transform.GetChild(0).position + new Vector3(0f, 0.2f, 0f), Targets.transform.GetChild(0).rotation, targets);
        // targets.Translate(objsPosition);
        // targets.Rotate(rotation.eulerAngles);


        // Transform objs = new GameObject("ObjectsToBePlaced").transform;
        // objs.parent = sceneRoot;
        // objs.tag = "ObjectsToBePlaced";
        // Instantiate(ObjectsToBePlaced.transform.GetChild(0), ObjectsToBePlaced.transform.GetChild(0).position, ObjectsToBePlaced.transform.GetChild(0).rotation, objs);
        // objs.Translate(Vector3.Lerp(objsPosition, Camera.main.transform.position, 0.5f));


        // Counter.Instance.InitializeCounter(ObjectsToBePlaced.GetComponentsInChildren<Rigidbody>().Length);


        // Vector3 assistantPosition = gazePosition + new Vector3(-0.5f, 0f, 0f);
        // assistantPosition.y = floor.position.y;

        // if (assistantPresence != 0)
        // {
        //     Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation, sceneRoot);
        //     VirtualAssistantManager.Instance.patience = assistantPatience;
        //     VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.25f * VirtualAssistantManager.Instance.transform.localScale.x, 0.25f * VirtualAssistantManager.Instance.transform.localScale.y, 0.25f * VirtualAssistantManager.Instance.transform.localScale.z);
        // }

    }


    private void LoadSettings()
    {
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
        Destroy(GameObject.Find("Targets"));
    }
}
