
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBinAudioManager : BinAudioManagerInterface
{
    [AudioEvent]
    public string Paper;

    public override void PlayBinSound()
    {
        TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Paper);
    }
}
