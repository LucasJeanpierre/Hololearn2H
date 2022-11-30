using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationPlacementsEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ManipulationStarted(GameObject draggedObject)
    {
        if (PlacementsManager.Instance != null)
        {
            PlacementsManager.Instance.MakeLevelPrefabDisappear(draggedObject);
        }
    }

    public void ManipulationStopped()
    {
        if (PlacementsManager.Instance != null)
        {
            PlacementsManager.Instance.MakeLevelPrefabReapper();
        }
    }
}
