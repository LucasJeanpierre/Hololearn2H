using HoloLearn;

using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LayTheTableSettingsManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfLevel(int numberOfLevel)
    {
        LayTheTableSettings.Instance.numberOfLevel = numberOfLevel;
        RefreshMenu();
    }

    public void SetNumberOfPeople(int numberOfPeople)
    {
        LayTheTableSettings.Instance.numberOfPeople = numberOfPeople;
        RefreshMenu();
    }

    public void SetTargetsVisibility(int targetsVisibility)
    {
        LayTheTableSettings.Instance.targetsVisibility = targetsVisibility;
        RefreshMenu();
    }  


    public void RefreshMenu()
    {
        Interactable[] levelButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in levelButtons)
        {
            button.SetToggled(false);
        }
        levelButtons[LayTheTableSettings.Instance.numberOfLevel - 1].SetToggled(true);

        Interactable[] peopleButtons = gameObject.transform.transform.Find("PeopleButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in peopleButtons)
        {
            button.SetToggled(false);
        }
        peopleButtons[LayTheTableSettings.Instance.numberOfPeople - 1].SetToggled(true);

        Interactable targetCheckBox = gameObject.transform.Find("TargetCheckBox").GetComponent<Interactable>();
        if (LayTheTableSettings.Instance.targetsVisibility ==1)
        {
            targetCheckBox.SetToggled(true);
        }
        else
        {
            targetCheckBox.SetToggled(false);
        }

    }


    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("LayTheTableSettings",
                new XAttribute("NumberOfLevel", LayTheTableSettings.Instance.numberOfLevel),
                new XAttribute("NumberOfPeople", LayTheTableSettings.Instance.numberOfPeople),
                new XAttribute("TargetsVisibility", LayTheTableSettings.Instance.targetsVisibility));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
