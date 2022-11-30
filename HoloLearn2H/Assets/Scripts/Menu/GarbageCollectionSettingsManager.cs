using HoloLearn;

using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class GarbageCollectionSettingsManager : MonoBehaviour
{
    private PinchSlider wasteSlider;

    // Use this for initialization
    void Start()
    {
        wasteSlider = gameObject.transform.Find("WastePinchSlider").GetComponentInChildren<PinchSlider>();

        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfBins(int numberOfBins)
    {
       GarbageCollectionSettings.Instance.numberOfBins = numberOfBins;
       RefreshMenu();
    }
    
    public void SetNumberOfWaste()
    {
       GarbageCollectionSettings.Instance.numberOfWaste = Convert.ToInt32(wasteSlider.SliderValue * 10) + 3;
    }

 
    public void RefreshMenu()
    {
        Interactable[] binsButtons = gameObject.transform.Find("BinsButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in binsButtons)
        {
            button.SetToggled(false);
        }
        binsButtons[GarbageCollectionSettings.Instance.numberOfBins - 1].SetToggled(true);

        wasteSlider.SliderValue = (float)(GarbageCollectionSettings.Instance.numberOfWaste - 3) / 10;
        RefreshSliderText();
    }

    public void RefreshSliderText()
    {
        int value = Convert.ToInt32(wasteSlider.SliderValue * 10) + 3;
        wasteSlider.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = $"{value}";
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("GarbageCollectionSettings",
                new XAttribute("NumberOfBins", GarbageCollectionSettings.Instance.numberOfBins),
                new XAttribute("NumberOfWaste", GarbageCollectionSettings.Instance.numberOfWaste));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
