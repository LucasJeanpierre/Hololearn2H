
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketAudioManager : MonoBehaviour
{
    [AudioEvent]
    public string BasketDunk1;
    [AudioEvent]
    public string BasketDunk2;

    public void PlayBasketDunk()
    {
        System.Random rnd = new System.Random();
        switch (rnd.Next(0, 1))
        {
            case 0:
                TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BasketDunk1);
                break;
            case 1:
                TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(BasketDunk2);
                break;
        }
    }
}
