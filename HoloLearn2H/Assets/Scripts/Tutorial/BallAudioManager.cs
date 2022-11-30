using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioManager : MonoBehaviour
{
    public AudioSource BallBump1;
    public AudioSource BallBump2;
    public AudioSource BallBump3;

    public void PlayBallBump()
    {
        System.Random rnd = new System.Random();
        switch (rnd.Next(0, 2))
        {
            case 0:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BallBump1);
                BallBump1.Play();
                break;
            case 1:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BallBump2);
                BallBump2.Play();
                break;
            case 2:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BallBump3);
                BallBump3.Play();
                break;
        }
    }
}
