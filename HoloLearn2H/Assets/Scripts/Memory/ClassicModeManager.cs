

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassicModeManager : PlayModeManager
{
    public Transform firstElement;
    public Transform secondElement;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override void HandleTap(Transform selectedElement)
    {
        selectedElement.GetChild(0).gameObject.SetActive(false);
        selectedElement.GetChild(1).gameObject.SetActive(true);

        if (firstElement != null)
        {
            IsBusy = true;

            secondElement = selectedElement.GetChild(1);
            if (firstElement.gameObject.name == secondElement.gameObject.name)
            {
                firstElement = null;
                secondElement = null;

                Counter.Instance.Decrement();
                Counter.Instance.Decrement();

                if (VirtualAssistantManager.Instance != null)
                {
                    VirtualAssistantManager.Instance.Jump();
                }

                IsBusy = false;
            }
            else
            {
                IsBusy = true;

                if (VirtualAssistantManager.Instance != null)
                {
                    VirtualAssistantManager.Instance.ShakeHead();
                }

                StartCoroutine(Wait(selectedElement));
            }
        }
        else
        {
            firstElement = selectedElement.GetChild(1);
        }

    }


    private IEnumerator Wait(Transform selectedElement)
    {
        yield return new WaitForSeconds(3f);

        firstElement.parent.GetChild(0).gameObject.SetActive(true);
        firstElement.gameObject.SetActive(false);
        secondElement.parent.GetChild(0).gameObject.SetActive(true);
        secondElement.gameObject.SetActive(false);

        firstElement = null;
        secondElement = null;

        IsBusy = false;
    }


    public override List<Transform> GenerateObjects(GameObject ObjectsPrefabs, int numberOfBoxes)
    {
        System.Random rnd = new System.Random();

        List<Transform> objs = new List<Transform>();
        List<string> createdObjs = new List<string>();
        for (int i = 1; i <= numberOfBoxes / 2;)
        {
            int j = rnd.Next(0, ObjectsPrefabs.transform.childCount);
            Transform obj = ObjectsPrefabs.transform.GetChild(j);

            if (!createdObjs.Contains(ObjectsPrefabs.transform.GetChild(j).name))
            {
                objs.Add(obj);
                objs.Add(obj);
                createdObjs.Add(ObjectsPrefabs.transform.GetChild(j).name);
                i++;
            }
        }

        Counter.Instance.InitializeCounter(objs.Count);

        return objs;
    }


    public override void StartGame(int waitingTime)
    {
        StartCoroutine(ShowObjects(waitingTime));
    }

    private IEnumerator ShowObjects(int waitingTime)
    {
        Transform elems = GameObject.Find("Elements").transform;

        for (int i = 0; i < elems.childCount; i++)
        {
            elems.GetChild(i).GetChild(0).gameObject.SetActive(false);
            elems.GetChild(i).GetChild(1).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(waitingTime);

        for (int i = 0; i < elems.childCount; i++)
        {
            elems.GetChild(i).GetChild(0).gameObject.SetActive(true);
            elems.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }
}
