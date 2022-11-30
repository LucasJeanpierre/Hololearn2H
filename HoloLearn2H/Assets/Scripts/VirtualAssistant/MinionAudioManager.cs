
using UnityEngine;

public class MinionAudioManager : AssistantAudioManagerInterface
{
    public AudioSource ShakingHeadNo;
    public AudioSource Jump;


    public override void PlayShakingHeadNo()
    {
        //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(ShakingHeadNo);
        ShakingHeadNo.Play();
    }

    public override void PlayJump()
    {
        //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Jump);
        Jump.Play();
    }

    
    public override void PlayWalking()
    {

    }

    public override void PlayIntro()
    {

    }

    public override void PlayPointing()
    {

    }

}