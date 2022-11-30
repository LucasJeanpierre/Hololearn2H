
using UnityEngine;

public abstract class AssistantAudioManagerInterface : MonoBehaviour
{

    public void Start()
    {

    }

    public abstract void PlayShakingHeadNo();

    public abstract void PlayJump();

    public abstract void PlayIntro();

    public abstract void PlayWalking();

    public abstract void PlayPointing();

}