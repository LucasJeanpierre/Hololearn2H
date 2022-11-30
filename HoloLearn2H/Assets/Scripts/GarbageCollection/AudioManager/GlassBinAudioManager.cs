
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBinAudioManager : BinAudioManagerInterface
{
    [AudioEvent]
    public string Glass;

    public override void PlayBinSound()
    {
        TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Glass);
    }
}
