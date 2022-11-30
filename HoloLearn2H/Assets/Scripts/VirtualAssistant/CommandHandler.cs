using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void Help()
    {
        VirtualAssistantManager.Instance.CommandReceived();
    }

    public void Talk()
    {
        VirtualAssistantManager.Instance.ExplainTaskGoal();
    }

}
