using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListSettingsManager : MonoBehaviour {

    public GameObject PlayerEntry;
    private TouchScreenKeyboard keyboard;
    private string keyboardText;

    // Use this for initialization
    void Start()
    {
        RefreshMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard != null)
        {
            keyboardText = keyboard.text;
        }
    }

    public void RefreshMenu()
    {
        Transform playersList = GameObject.Find("PlayersList").transform;

        for (int i=0; i < playersList.childCount; i++)
        {
            Destroy(playersList.GetChild(i).gameObject);
        }

        Vector3 offset = new Vector3();
        for (int i = 0; i < PlayerListSettings.Instance.listOfPlayers.Count; i++)
        {
            GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);
            offset += new Vector3(0f, -0.07f, 0f);

            entry.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMesh>().text = PlayerListSettings.Instance.listOfPlayers.ElementAt(i);
        }

        for (int i = 0; i < gameObject.transform.Find("PlayersList").childCount; i++)
        {
            gameObject.transform.Find("PlayersList").GetChild(i).GetChild(0).GetComponent<Interactable>().SetToggled(i == PlayerListSettings.Instance.currentPlayer);
        }
    }

    public void DeletePlayerEntry(GameObject entry)
    {
        Transform playersList = GameObject.Find("PlayersList").transform;
        for (int i = 0; i < playersList.childCount; i++)
        {
            if (playersList.GetChild(i).gameObject.GetInstanceID() == entry.transform.GetChild(0).GetInstanceID())
            {
                Destroy(playersList.GetChild(i).gameObject.gameObject);
                PlayerListSettings.Instance.listOfPlayers.RemoveAt(i);
            }
        }

        string playerName = entry.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMesh>().text;

        int playerIndex = PlayerListSettings.Instance.listOfPlayers.IndexOf(playerName);
        PlayerListSettings.Instance.listOfPlayers.Remove(playerName);

        SettingsFileManager.Instance.DeletePlayerSettings(playerIndex);

        if (playerIndex >= PlayerListSettings.Instance.currentPlayer)
        {
            PlayerListSettings.Instance.currentPlayer--;  
        }
        SettingsFileManager.Instance.UpdatePlayerSelectionSettings(PlayerListSettings.Instance.currentPlayer);
        
        RefreshMenu();
    }

    public void AddPlayerEntry()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);

        string playerName = keyboardText;

        PlayerListSettings.Instance.listOfPlayers.Add(playerName);

        Transform playersList = GameObject.Find("PlayersList").transform;
        Vector3 offset = new Vector3(0f, -0.07f * playersList.childCount, 0f);
        GameObject entry = Instantiate(PlayerEntry, playersList.transform.position + offset, playersList.transform.rotation, playersList);

        entry.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMesh>().text = playerName;

        PlayerListSettings.Instance.currentPlayer = PlayerListSettings.Instance.listOfPlayers.IndexOf(playerName);
        SettingsFileManager.Instance.UpdatePlayerSelectionSettings(PlayerListSettings.Instance.currentPlayer);

        LayTheTableSettings.Instance.numberOfLevel = 1;
        LayTheTableSettings.Instance.numberOfPeople = 1;
        LayTheTableSettings.Instance.targetsVisibility = 1;
        GarbageCollectionSettings.Instance.numberOfBins = 2;
        GarbageCollectionSettings.Instance.numberOfWaste = 5;
        VirtualAssistantChoice.Instance.assistantPresence = 1;
        VirtualAssistantChoice.Instance.selectedAssistant = 0;
        VirtualAssistantSettings.Instance.assistantBehaviour = 1;
        VirtualAssistantSettings.Instance.assistantPatience = 5;

        SettingsFileManager.Instance.AddPlayerSettings();
    }

    public void UpdatePlayerSelection(GameObject selectedEntry)
    {
        Interactable[] buttons = gameObject.transform.Find("PlayersList").GetComponentsInChildren<Interactable>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.GetInstanceID() == selectedEntry.transform.GetChild(0).gameObject.GetInstanceID())
            {
                buttons[i].SetToggled(true);
                PlayerListSettings.Instance.currentPlayer = i;
            }
            else
            {
                buttons[i].SetToggled(false);
            }
        }

        SettingsFileManager.Instance.UpdatePlayerSelectionSettings(PlayerListSettings.Instance.currentPlayer);
    }

}
