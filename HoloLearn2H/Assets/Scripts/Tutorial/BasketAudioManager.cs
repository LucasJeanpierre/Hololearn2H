
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketAudioManager : MonoBehaviour
{
    
    public AudioSource BasketDunk1;
    
    public AudioSource BasketDunk2;

    public void PlayBasketDunk()
    {
        System.Random rnd = new System.Random();
        switch (rnd.Next(0, 1))
        {
            case 0:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BasketDunk1);
                BasketDunk1.Play();
                break;
            case 1:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BasketDunk2);
                BasketDunk2.Play();
                break;
        }
    }
}
