using System.Collections;
using UnityEngine;

public class PlacementCollisionManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            //other.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled = false;

            Counter.Instance.Decrement();

            if (VirtualAssistantManager.Instance != null)
            {
                VirtualAssistantManager.Instance.Jump();
                VirtualAssistantManager.Instance.ObjectDropped();
            }
            
            if (other.gameObject.GetComponent<ObjectPositionManager>() != null)
            {
                other.gameObject.GetComponent<ObjectPositionManager>().HasCollided(transform);
            }

            Destroy(gameObject);
        }
        else
        {
            if (other.gameObject.tag != "Untagged" && !VirtualAssistantManager.Instance.IsBusy)
            {
                VirtualAssistantManager.Instance.ShakeHead();
            }
        }
    }
    
}
