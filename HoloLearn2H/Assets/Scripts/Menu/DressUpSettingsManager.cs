using HoloLearn;

using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class DressUpSettingsManager : MonoBehaviour
{
    private PinchSlider clothesSlider;

    // Use this for initialization
    void Start()
    {
        clothesSlider = gameObject.transform.Find("ClothesPinchSlider").GetComponentInChildren<PinchSlider>();

        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfLevel(int numberOfLevel)
    {
        DressUpSettings.Instance.numberOfLevel = numberOfLevel;
        RefreshMenu();
    }

    public void SetNumberOfClothes()
    {
        DressUpSettings.Instance.numberOfClothes = Convert.ToInt32(clothesSlider.SliderValue * 10) + 3;
    }


    public void RefreshMenu()
    {
        Interactable[] levelsButtons = gameObject.transform.Find("LevelsButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in levelsButtons)
        {
            button.SetToggled(false);
        }
        levelsButtons[DressUpSettings.Instance.numberOfLevel - 1].SetToggled(true);

        clothesSlider.SliderValue = (float)(DressUpSettings.Instance.numberOfClothes - 3) / 10;
        RefreshSliderText();
    }

    public void RefreshSliderText()
    {
        int value = Convert.ToInt32(clothesSlider.SliderValue * 10) + 3;
        clothesSlider.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = $"{value}";
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("DressUpSettings",
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfLevel),
                new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfClothes));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
