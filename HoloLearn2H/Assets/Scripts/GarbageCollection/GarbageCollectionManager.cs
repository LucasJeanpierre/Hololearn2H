


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GarbageCollectionManager : TaskManager
{

    public GameObject BinsPrefabs;
    public GameObject WastePrefabs;
    public GameObject VirtualAssistantsPrefabs;

    private int numberOfBins;
    private int numberOfWaste;
    private int assistantPresence;
    private int selectedAssistant;
    private int explainTaskGoal;
    private int assistantBehaviour;
    private int assistantPatience;

    private Transform virtualAssistant;

    public List<string> activeBins;

    // Use this for initialization
    public override void Start()
    {
        LoadSettings();

        virtualAssistant = VirtualAssistantsPrefabs.transform.GetChild(selectedAssistant+1).GetChild(assistantBehaviour-1);
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

        System.Random rnd = new System.Random();


      
        Vector3 floorPosition = floor.transform.position + (plane.PlaneThickness * plane.SurfaceNormal);
        floorPosition = AdjustPositionWithSpatialMap(floorPosition, plane.SurfaceNormal);

        Vector3 gazePosition = new Vector3(0f, 0f, 0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f, Physics.DefaultRaycastLayers))
        {
            gazePosition = hitInfo.point;
        }

        Vector3 binsPosition = gazePosition;
        binsPosition.y = floorPosition.y;


        Vector3 relativePos = Camera.main.transform.position - gazePosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;


        Transform sceneRoot = GameObject.Find("Broadcasted Content").transform;

        Transform bins = new GameObject("Bins").transform;
        bins.parent = sceneRoot;
        bins.tag = "Targets";

        activeBins = new List<string>();
        for (int i=1; i<=numberOfBins;)
        {
            Transform bin = BinsPrefabs.transform.GetChild(rnd.Next(0, BinsPrefabs.transform.childCount));
            string currentBinTag = bin.gameObject.tag;
            if (!activeBins.Contains(currentBinTag))
            {
                Instantiate(bin, new Vector3((float)Math.Pow(-1, i) * 0.4f * (i / 2), 0f, 0f), bin.rotation, bins);
                activeBins.Add(bin.gameObject.tag);
                i++;
            }
        }

        bins.Translate(binsPosition);        
        bins.Rotate(rotation.eulerAngles);


        Transform waste = new GameObject("Waste").transform;
        waste.parent = sceneRoot;
        waste.tag = "ObjectsToBePlaced";

        Vector3 wastePosition = Vector3.Lerp(Camera.main.transform.position, bins.position, 0.5f);
        wastePosition.y = floorPosition.y + 0.1f;

        for (int i=0; i<numberOfWaste;)
        {
            Transform wasteGroup = WastePrefabs.transform.GetChild(rnd.Next(0, WastePrefabs.transform.childCount));
            int groupSize = wasteGroup.GetComponentsInChildren<Rigidbody>().Length;
            Transform currentWaste = wasteGroup.GetChild(rnd.Next(0, groupSize));
            string currentWasteTag = currentWaste.gameObject.tag;
            if (activeBins.Contains(currentWasteTag))
            {
                Instantiate(currentWaste.gameObject, currentWaste.position, currentWaste.rotation, waste);
                i++;
            }
        }

        waste.Translate(wastePosition);
        waste.Rotate(rotation.eulerAngles);


        Counter.Instance.InitializeCounter(waste.GetComponentsInChildren<Rigidbody>().Length);


        Vector3 assistantPosition = bins.TransformPoint(-0.3f, 0f, 0.3f);
        assistantPosition.y = floor.position.y;

        if (assistantPresence != 0)
        {
            Instantiate(virtualAssistant.gameObject, assistantPosition, virtualAssistant.transform.rotation, sceneRoot);
            VirtualAssistantManager.Instance.patience = assistantPatience;
            VirtualAssistantManager.Instance.transform.localScale += new Vector3(0.25f * VirtualAssistantManager.Instance.transform.localScale.x, 0.25f * VirtualAssistantManager.Instance.transform.localScale.y, 0.25f * VirtualAssistantManager.Instance.transform.localScale.z);

            if (explainTaskGoal == 1)
            {
                VirtualAssistantManager.Instance.ExplainTaskGoal();
            }
        }

    }


    private void LoadSettings()
    {
        numberOfBins = GarbageCollectionSettings.Instance.numberOfBins;
        numberOfWaste = GarbageCollectionSettings.Instance.numberOfWaste;
        assistantPresence = VirtualAssistantChoice.Instance.assistantPresence;
        selectedAssistant = VirtualAssistantChoice.Instance.selectedAssistant;
        explainTaskGoal = VirtualAssistantSettings.Instance.explainTaskGoal;
        assistantBehaviour = VirtualAssistantSettings.Instance.assistantBehaviour;
        assistantPatience = VirtualAssistantSettings.Instance.assistantPatience;
    }


    public override void DestroyObjects()
    {
        if (VirtualAssistantManager.Instance != null)
        {
            Destroy(VirtualAssistantManager.Instance.gameObject);
        }
        Destroy(GameObject.Find("Bins"));
        Destroy(GameObject.Find("Waste"));
    }
}
