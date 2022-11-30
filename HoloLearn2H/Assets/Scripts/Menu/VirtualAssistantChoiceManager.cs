using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class VirtualAssistantChoiceManager : MonoBehaviour
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

    public void SetAssistantPresence(int assistantPresence)
    {
        VirtualAssistantChoice.Instance.assistantPresence = assistantPresence;
        RefreshMenu();
    }

    public void LeftArrowClicked()
    {
        VirtualAssistantChoice.Instance.selectedAssistant--;
        RefreshMenu();
    }

    public void RightArrowClicked()
    {
        VirtualAssistantChoice.Instance.selectedAssistant++;
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        Interactable assistantCheckBox = gameObject.transform.Find("AssistantCheckBox").GetComponent<Interactable>();
        if (VirtualAssistantChoice.Instance.assistantPresence == 1)
        {
            assistantCheckBox.SetToggled(true);

            Animator[] assistants = GameObject.Find("VirtualAssistants").GetComponentsInChildren<Animator>(true);
            for (int i = 0; i < assistants.Length; i++)
            {
                assistants[i].gameObject.SetActive(false);
            }
            assistants[VirtualAssistantChoice.Instance.selectedAssistant].gameObject.SetActive(true);

            GameObject.Find("AssistantPanel").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("AssistantPanel").transform.GetChild(2).gameObject.SetActive(true);
            if (VirtualAssistantChoice.Instance.selectedAssistant == 0)
            {
                GameObject.Find("LeftArrow").SetActive(false);
            }
            if (VirtualAssistantChoice.Instance.selectedAssistant == GameObject.Find("VirtualAssistants").transform.childCount - 1)
            {
                GameObject.Find("RightArrow").SetActive(false);
            }
        }
        else
        {
            assistantCheckBox.SetToggled(false);
        }
    }


    public void SaveSettings()
    {
        XElement newSettings =
            new XElement("VirtualAssistantChoice",
                new XAttribute("AssistantPresence", VirtualAssistantChoice.Instance.assistantPresence),
                new XAttribute("SelectedAssistant", VirtualAssistantChoice.Instance.selectedAssistant));

        SettingsFileManager.Instance.UpdatePlayerSettings(newSettings);
    }
}
