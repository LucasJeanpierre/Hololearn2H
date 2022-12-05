using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCollisionManager : MonoBehaviour
{
    Vector3 floorPosition;

    // Use this for initialization
    void Start()
    {
        floorPosition = Vector3.zero;//GameObject.Find("SurfacePlane(Clone)").transform.position;
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
        if (other.gameObject.name != "SurfacePlane(Clone)")
        {
            TagsContainer tagsContainer = other.transform.GetComponent<TagsContainer>();
            if (tagsContainer == null)
            {
                return;
            }

            List<string> tags = tagsContainer.tags;
            string weather = GameObject.Find("Weather").transform.GetChild(0).GetChild(0).tag;
            string temperature = GameObject.Find("Weather").transform.GetChild(0).GetChild(1).tag;

            foreach (string tag in tags)
            {
                if (tags.Contains(weather) || tags.Contains(temperature))
                {
                    Counter.Instance.Decrement();

                    if (VirtualAssistantManager.Instance != null)
                    {
                        VirtualAssistantManager.Instance.Jump();
                        VirtualAssistantManager.Instance.ObjectDropped();
                    }

                    other.transform.GetComponent<ObjectPositionManager>().HasCollided(transform);
                }
                else
                {
                    if (VirtualAssistantManager.Instance != null && !VirtualAssistantManager.Instance.IsBusy)
                    {
                        VirtualAssistantManager.Instance.ShakeHead();
                    }
                }
            }         
        }
    }

}
