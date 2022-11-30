
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class VirtualAssistantManager : Singleton<VirtualAssistantManager>
{
    public float speed;
    public int patience;
    public bool IsDragging;
    public bool IsBusy;
    public Transform targetObject;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public virtual void Jump()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Jump");
    }

    public virtual void ShakeHead()
    {
        gameObject.GetComponent<Animator>().SetTrigger("ShakeHead");
    }

    public abstract void ObjectDragged(GameObject draggedObject);

    public abstract void ObjectDropped();

    public abstract void Walk();

    public abstract void SetTriggers();

    public abstract void CommandReceived();

    public virtual void ExplainTaskGoal()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Talk");
    }

}
