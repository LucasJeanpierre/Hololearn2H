
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBinAudioManager : BinAudioManagerInterface
{
    public AudioSource Plastic;

    public override void PlayBinSound()
    {
        //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Plastic);
        Plastic.Play();
    }

}
