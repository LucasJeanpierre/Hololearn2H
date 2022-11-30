using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntryButtonsHandler : MonoBehaviour {

    public void DeletePlayerEntry(GameObject selectedButton)
    {
        GameObject.Find("PlayerMenu").GetComponent<PlayerListSettingsManager>().DeletePlayerEntry(selectedButton);
    }

    public void UpdatePlayerSelection(GameObject selectedButton)
    {
        GameObject.Find("PlayerMenu").GetComponent<PlayerListSettingsManager>().UpdatePlayerSelection(selectedButton);
    }
}
