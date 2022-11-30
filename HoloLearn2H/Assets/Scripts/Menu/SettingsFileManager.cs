using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;

using System.Xml.Linq;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif


public class SettingsFileManager : Singleton<SettingsFileManager>
{

    public XElement LoadFile()
    {
        XElement root = null;

#if !UNITY_EDITOR && UNITY_METRO
        
        Task<Task> task = new Task<Task>(async () =>
            {
                try
                {
		            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    root = XElement.Parse(xmlText);
                    //Debug.Log(root);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait(); 
        task.Result.Wait();

#endif

        return root;
    }


    public int LoadCurrentPlayerSelection()
    {
        int currentPlayerSelection = transform.GetComponent<PlayerListSettings>().currentPlayer; ;

#if !UNITY_EDITOR && UNITY_METRO
        
        XElement root = LoadFile();
        
        IEnumerable<XElement> players =
            from item in root.Elements("Player")
            select item;

        PlayerListSettings.Instance.listOfPlayers.Clear();
        foreach (XElement item in players)
        {
            PlayerListSettings.Instance.listOfPlayers.Add((string)item.Attribute("PlayerName"));
        }
        PlayerListSettings.Instance.currentPlayer = (int)root.Attribute("CurrentPlayer");

        currentPlayerSelection = (int)root.Attribute("CurrentPlayer");
        
#endif

        return currentPlayerSelection;
    }

    public void LoadCurrentPlayerSettings(int playerIndex)
    {
#if !UNITY_EDITOR && UNITY_METRO

        XElement root = LoadFile();

        IEnumerable<XElement> layTheTableSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("LayTheTableSettings");

        LayTheTableSettings.Instance.numberOfLevel = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfLevel");
        LayTheTableSettings.Instance.numberOfPeople = (int)layTheTableSettings.ElementAt(0).Attribute("NumberOfPeople");
        LayTheTableSettings.Instance.targetsVisibility = (int)layTheTableSettings.ElementAt(0).Attribute("TargetsVisibility");

        IEnumerable<XElement> garbageCollectionSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("GarbageCollectionSettings");

        GarbageCollectionSettings.Instance.numberOfBins = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfBins");
        GarbageCollectionSettings.Instance.numberOfWaste = (int)garbageCollectionSettings.ElementAt(0).Attribute("NumberOfWaste");

        IEnumerable<XElement> dressUpSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("DressUpSettings");

        DressUpSettings.Instance.numberOfLevel = (int)dressUpSettings.ElementAt(0).Attribute("NumberOfLevel");
        DressUpSettings.Instance.numberOfClothes = (int)dressUpSettings.ElementAt(0).Attribute("NumberOfClothes");

        IEnumerable<XElement> memorySettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("MemorySettings");

        MemorySettings.Instance.playMode = (int)memorySettings.ElementAt(0).Attribute("PlayMode");
        MemorySettings.Instance.numberOfBoxes = (int)memorySettings.ElementAt(0).Attribute("NumberOfBoxes");
        MemorySettings.Instance.waitingTime = (int)memorySettings.ElementAt(0).Attribute("WaitingTime");

        IEnumerable<XElement> virtualAssistantChoice =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("VirtualAssistantChoice");

        VirtualAssistantChoice.Instance.assistantPresence = (int)virtualAssistantChoice.ElementAt(0).Attribute("AssistantPresence");
        VirtualAssistantChoice.Instance.selectedAssistant = (int)virtualAssistantChoice.ElementAt(0).Attribute("SelectedAssistant");

        IEnumerable<XElement> virtualAssistantSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(playerIndex)
            select item.Element("VirtualAssistantSettings");

        VirtualAssistantSettings.Instance.explainTaskGoal = (int)virtualAssistantSettings.ElementAt(0).Attribute("ExplainTaskGoal");
        VirtualAssistantSettings.Instance.assistantBehaviour = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantBehaviour");
        VirtualAssistantSettings.Instance.assistantPatience = (int)virtualAssistantSettings.ElementAt(0).Attribute("AssistantPatience");

#endif
    }



    public void UpdateFile(XElement root)
    {
#if !UNITY_EDITOR && UNITY_METRO
		  
       Task<Task> task = new Task<Task>(async () =>
            {   
                try
                {
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.GetFileAsync("settings.xml");
                    string xmlText = root.ToString();
                    await FileIO.WriteTextAsync(xmlFile, xmlText);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait(); 
        task.Result.Wait();
        
#endif
    }


    public void CreateFileIfNotExists()
    {
#if !UNITY_EDITOR && UNITY_METRO
		
        Task<Task> task = new Task<Task>(async () =>
            {   
                try
                { 
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlFile = await storageFolder.CreateFileAsync("settings.xml", CreationCollisionOption.OpenIfExists);
                    string xmlText = await FileIO.ReadTextAsync(xmlFile);
                    if (xmlText == "")
                    {
                        XElement root =
                            new XElement("Players",
                            new XAttribute("CurrentPlayer", 1),
                                new XElement("Player",
                                new XAttribute("PlayerName", "Player 1"),
                                    new XElement("LayTheTableSettings",
                                        new XAttribute("NumberOfLevel", 3),
                                        new XAttribute("NumberOfPeople", 1),
                                        new XAttribute("TargetsVisibility", 1)),
                                    new XElement("GarbageCollectionSettings",
                                        new XAttribute("NumberOfBins", 2),
                                        new XAttribute("NumberOfWaste", 5)),
                                    new XElement("DressUpSettings",
                                        new XAttribute("NumberOfLevel", 1),
                                        new XAttribute("NumberOfClothes", 3)),
                                    new XElement("MemorySettings",
                                        new XAttribute("PlayMode", 0),
                                        new XAttribute("NumberOfBoxes", 6),
                                        new XAttribute("WaitingTime", 3)),
                                    new XElement("VirtualAssistantChoice",
                                        new XAttribute("AssistantPresence", 0),
                                        new XAttribute("SelectedAssistant", 1)),
                                    new XElement("VirtualAssistantSettings",
                                        new XAttribute("ExplainTaskGoal", 0),
                                        new XAttribute("AssistantBehaviour", 2),
                                        new XAttribute("AssistantPatience", 5))),
                            new XElement("Player",
                                new XAttribute("PlayerName", "Player 2"),
                                    new XElement("LayTheTableSettings",
                                        new XAttribute("NumberOfLevel", 1),
                                        new XAttribute("NumberOfPeople", 3),
                                        new XAttribute("TargetsVisibility", 0)),
                                    new XElement("GarbageCollectionSettings",
                                        new XAttribute("NumberOfBins", 3),
                                        new XAttribute("NumberOfWaste", 8)),
                                    new XElement("DressUpSettings",
                                        new XAttribute("NumberOfLevel", 2),
                                        new XAttribute("NumberOfClothes", 4)),
                                    new XElement("MemorySettings",
                                        new XAttribute("PlayMode", 0),
                                        new XAttribute("NumberOfBoxes", 8),
                                        new XAttribute("WaitingTime", 3)),
                                    new XElement("VirtualAssistantChoice",
                                        new XAttribute("AssistantPresence", 1),
                                        new XAttribute("SelectedAssistant", 0)),
                                    new XElement("VirtualAssistantSettings",
                                        new XAttribute("ExplainTaskGoal", 1),
                                        new XAttribute("AssistantBehaviour", 1),
                                        new XAttribute("AssistantPatience", 5))));
                        xmlText = root.ToString();   
                        await FileIO.WriteTextAsync(xmlFile, xmlText);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        task.Start();
        task.Wait(); 
        task.Result.Wait();
        
#endif
    }


    public void UpdatePlayerSettings(XElement newSettings)
    {
#if !UNITY_EDITOR && UNITY_METRO

        XElement root = LoadFile();

        IEnumerable<XElement> oldSettings =
            from item in root.Elements("Player")
            where item.Attribute("PlayerName").Value == PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)
            select item.Element(newSettings.Name);

        oldSettings.ElementAt(0).ReplaceWith(newSettings);
        UpdateFile(root);
        Debug.Log(root);

#endif
    }

    public void AddPlayerSettings()
    {
#if !UNITY_EDITOR && UNITY_METRO

        XElement root = LoadFile();
        root.SetAttributeValue("CurrentPlayer", PlayerListSettings.Instance.currentPlayer);

        XElement newPlayer =
            new XElement("Player",
            new XAttribute("PlayerName", PlayerListSettings.Instance.listOfPlayers.ElementAt(PlayerListSettings.Instance.currentPlayer)),
                new XElement("LayTheTableSettings",
                    new XAttribute("NumberOfLevel", LayTheTableSettings.Instance.numberOfLevel),
                    new XAttribute("NumberOfPeople", LayTheTableSettings.Instance.numberOfPeople),
                    new XAttribute("TargetsVisibility", LayTheTableSettings.Instance.targetsVisibility)),
                new XElement("GarbageCollectionSettings",
                    new XAttribute("NumberOfBins", GarbageCollectionSettings.Instance.numberOfBins),
                    new XAttribute("NumberOfWaste", GarbageCollectionSettings.Instance.numberOfWaste)),
                new XElement("DressUpSettings",
                    new XAttribute("NumberOfLevel", DressUpSettings.Instance.numberOfLevel),
                    new XAttribute("NumberOfClothes", DressUpSettings.Instance.numberOfClothes)),
                new XElement("MemorySettings",
                    new XAttribute("PlayMode", MemorySettings.Instance.playMode),
                    new XAttribute("NumberOfBoxes", MemorySettings.Instance.numberOfBoxes),
                    new XAttribute("WaitingTime", MemorySettings.Instance.waitingTime)),
                new XElement("VirtualAssistantChoice",
                    new XAttribute("AssistantPresence", VirtualAssistantChoice.Instance.assistantPresence),
                    new XAttribute("SelectedAssistant", VirtualAssistantChoice.Instance.selectedAssistant)),
                new XElement("VirtualAssistantSettings",
                    new XAttribute("ExplainTaskGoal", VirtualAssistantSettings.Instance.explainTaskGoal),
                    new XAttribute("AssistantBehaviour", VirtualAssistantSettings.Instance.assistantBehaviour),
                    new XAttribute("AssistantPatience", VirtualAssistantSettings.Instance.assistantPatience)));

        root.Add(newPlayer);
        UpdateFile(root);

#endif
    }

    public void DeletePlayerSettings(int playerIndex)
    {
#if !UNITY_EDITOR && UNITY_METRO

        XElement root = LoadFile();

        IEnumerable<XElement> players =
            from item in root.Elements("Player")
            select item;

        players.ElementAt(playerIndex).Remove();
        UpdateFile(root);

#endif
    }

    public void UpdatePlayerSelectionSettings(int playerIndex)
    {
#if !UNITY_EDITOR && UNITY_METRO

        XElement root = LoadFile();
        root.SetAttributeValue("CurrentPlayer", playerIndex);
        UpdateFile(root);
        LoadCurrentPlayerSettings(playerIndex);

#endif
    }
}
