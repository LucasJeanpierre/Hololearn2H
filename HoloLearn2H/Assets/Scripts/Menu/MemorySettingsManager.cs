using HoloLearn;

using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class MemorySettingsManager : MonoBehaviour
{
    private PinchSlider timeSlider;

    // Use this for initialization
    void Start()
    {
        timeSlider = gameObject.transform.Find("TimePinchSlider").GetComponentInChildren<PinchSlider>();

        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayMode(int playMode)
    {
        MemorySettings.Instance.playMode = playMode;
        RefreshMenu();
    }

    public void SetNumberOfBoxes(int numberOfBoxes)
    {
        MemorySettings.Instance.numberOfBoxes = numberOfBoxes;
        RefreshMenu();
    }

    public void SetWaitingTime()
    {
        MemorySettings.Instance.waitingTime = Convert.ToInt32(timeSlider.SliderValue * 10);
    }


    public void RefreshMenu()
    {
        Interactable[] playModeButtons = gameObject.transform.Find("PlayModeButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in playModeButtons)
        {
            button.SetToggled(false);
        }
        playModeButtons[MemorySettings.Instance.playMode].SetToggled(true);

        Interactable[] boxesButtons = gameObject.transform.Find("BoxesButtons").GetComponentsInChildren<Interactable>();
        foreach (Interactable button in boxesButtons)
        {
            button.SetToggled(false);
        }
        boxesButtons[(MemorySettings.Instance.numberOfBoxes - 4) / 2 - 1].SetToggled(true);

        timeSlider.SliderValue = (float)(MemorySettings.Instance.waitingTime) / 10;
        RefreshSliderText();
    }

    public void RefreshSliderText()
    {
        int value = Convert.ToInt32(timeSlider.SliderValue * 10);
        timeSlider.transform.GetChild(0).GetChild(1).GetComponent<TextMesh>().text = $"{value}";
    }

    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("MemorySettings",
                new XAttribute("PlayMode", MemorySettings.Instance.playMode),
                new XAttribute("NumberOfBoxes", MemorySettings.Instance.numberOfBoxes),
                new XAttribute("WaitingTime", MemorySettings.Instance.waitingTime));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
