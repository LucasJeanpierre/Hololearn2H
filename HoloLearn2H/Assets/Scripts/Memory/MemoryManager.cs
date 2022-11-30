

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryManager : TaskManager
{
    public GameObject BoxPrefab;
    public GameObject ObjectsPrefabs;
    public GameObject PlayModesPrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int playMode;
    private int numberOfBoxes;
    private int waitingTime;
    private int assistantPresence;
    private int selectedAssistant;

    private Transform virtualAssistant;

    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        Instantiate(PlayModesPrefabs.transform.GetChild(playMode), GameObject.Find("MemoryManager").transform);

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant + 1).GetChild(0);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void GenerateObjectsInWorld()
    {
        //Seleziono il pavimento
        Transform floor = SpatialProcessing.Instance.floors.ElementAt(0).transform;
        SurfacePlane plane = floor.GetComponent<SurfacePlane>();


        Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);

        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
        }

        Vector3 boxesPosition = gazePosition;
        boxesPosition.y = floorPosition.y + 0.1f;


        Vector3 relativePos = gazePosition - Camera.main.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;


        List<Transform> objs = transform.GetComponentInChildren<PlayModeManager>().GenerateObjects(ObjectsPrefabs, numberOfBoxes);

        Transform sceneRoot = GameObject.Find("Broadcasted Content").transform;

        System.Random rnd = new System.Random();
        Transform elems = new GameObject("Elements").transform;
        elems.parent = sceneRoot;
        for (int i = 1; i <= numberOfBoxes / 2; i++)
        {
            Transform elem = new GameObject("Element").transform;
            elem.parent = elems;
            elem.position = elems.TransformPoint(new Vector3((float)Math.Pow(-1, i) * 0.3f * (i / 2), 0f, 0f));
            GameObject box = Instantiate(BoxPrefab, elem.position, BoxPrefab.transform.rotation, elem);
            int j = rnd.Next(0, objs.Count);
            Transform obj = Instantiate(objs.ElementAt(j), box.transform.position, box.transform.rotation, elem);
            obj.gameObject.SetActive(false);
            objs.RemoveAt(j);

            Transform elem2 = new GameObject("Element").transform;
            elem2.parent = elems;
            elem2.position = elems.TransformPoint(new Vector3((float)Math.Pow(-1, i) * 0.3f * (i / 2), 0f, 0.3f));
            GameObject box2 = Instantiate(BoxPrefab, elem2.position, BoxPrefab.transform.rotation, elem2);
            int k = rnd.Next(0, objs.Count);
            Transform obj2 = Instantiate(objs.ElementAt(k), elem2.position, box2.transform.rotation, elem2);
            obj2.gameObject.SetActive(false);
            objs.RemoveAt(k);
        }

        elems.Translate(boxesPosition);
        elems.Rotate(rotation.eulerAngles);


        Vector3 assistantPosition = elems.GetChild(elems.childCount - 2).TransformPoint(0.3f * (float)Math.Pow(-1, elems.childCount / 2 % 2), 0f, 0f);
        assistantPosition.y = floor.position.y;

        if (assistantPresence != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation, sceneRoot);
            VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.25f * VirtualAssistantManager.Instance.transform.localScale.x, 0.25f * VirtualAssistantManager.Instance.transform.localScale.y, 0.25f * VirtualAssistantManager.Instance.transform.localScale.z);
        }

        transform.GetComponentInChildren<PlayModeManager>().StartGame(waitingTime);
        
    }

    
    private void LoadSettings()
    {
        playMode = MemorySettings.Instance.playMode;
        numberOfBoxes = MemorySettings.Instance.numberOfBoxes;
        waitingTime = MemorySettings.Instance.waitingTime;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
    }


    public override void DestroyObjects()
    {
        if (VirtualAssistantManager.Instance != null)
        {
            Destroy(VirtualAssistantManager.Instance.gameObject);
        }
        Destroy(GameObject.Find("Elements"));
    }

}
