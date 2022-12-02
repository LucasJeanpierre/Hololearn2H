
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBinAudioManager : BinAudioManagerInterface
{
    public AudioSource Glass;

    public override void PlayBinSound()
    {
        //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Glass);
        Glass.Play();
    }
}
