using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutleryPositionManager : LayTheTableObjectsPositionManager
{
    
    public override void AdjustTransform()
    {
        transform.Rotate(new Vector3(0f, 180f, 0f));
        transform.Translate(new Vector3(-0.05f, 0f, -0.01f));
    }
}
