using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPositionManager : LayTheTableObjectsPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Rotate(new Vector3(-90f, 0, 0f));
    }
}
