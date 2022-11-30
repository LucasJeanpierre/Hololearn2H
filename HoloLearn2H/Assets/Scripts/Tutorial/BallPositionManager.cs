using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionManager : ObjectPositionManager
{
    private bool hasCollided;
    private Vector3 targetPosition;
    private Vector3 floorPosition;


    public override void Start()
    {
        hasCollided = false;
        targetPosition = new Vector3();
        floorPosition = GameObject.Find("SurfacePlane(Clone)").transform.position;
    }

    public override void Update()
    {
        if (transform.position.y < floorPosition.y)
        {
            transform.position = new Vector3(transform.position.x, floorPosition.y + 0.01f, transform.position.z);
        }

        if (!hasCollided && !transform.GetComponent<Collider>().isTrigger && Math.Abs(transform.position.y - floorPosition.y) > 0.2f)
        {
            transform.GetComponent<Collider>().isTrigger = true;
        }

        if (hasCollided)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f || Math.Abs(transform.position.y - floorPosition.y) < 0.2f)
            {
                hasCollided = false;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.GetComponentInChildren<Collider>().enabled = true;
            }
        }
    }


    public override void HasCollided(Transform target)
    {
        transform.GetComponent<ManipulationEventHandler>().ManipulationStopped();
        transform.GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;

        targetPosition = target.TransformPoint(target.GetComponentInChildren<BoxCollider>().center);

        hasCollided = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SurfacePlane(Clone)")
        {
            transform.GetComponent<BallAudioManager>().PlayBallBump();
        }
        transform.GetComponent<Collider>().isTrigger = false;
    }
}
