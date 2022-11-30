using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePositionManager : LayTheTableObjectsPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Translate(new Vector3(0.06f, 0f, 0.06f));
    }
}
