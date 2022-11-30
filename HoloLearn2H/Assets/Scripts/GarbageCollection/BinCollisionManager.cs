using System.Collections;
using UnityEngine;

public class BinCollisionManager : MonoBehaviour
{
    Vector3 floorPosition;

    // Use this for initialization
    void Start()
    {
        floorPosition = GameObject.Find("SurfacePlane(Clone)").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < floorPosition.y)
        {
            transform.position = new Vector3(transform.position.x, floorPosition.y + 0.01f, transform.position.z);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Counter.Instance.Decrement();

            transform.GetComponent<BinAudioManagerInterface>().PlayBinSound();

            if (VirtualAssistantManager.Instance != null)
            {
                VirtualAssistantManager.Instance.Jump();
                VirtualAssistantManager.Instance.ObjectDropped();
            }

            other.transform.GetComponent<ObjectPositionManager>().HasCollided(transform);
        }
        else
        {
            GarbageCollectionManager manager = (GarbageCollectionManager)TaskManager.Instance;
            if (VirtualAssistantManager.Instance != null)
            {
                if (manager.activeBins.Contains(other.tag) && !VirtualAssistantManager.Instance.IsBusy)
                {
                    VirtualAssistantManager.Instance.ShakeHead();
                }
            }
        }
    }

}
