
using System.Collections;
using UnityEngine;

public class TalkingMinionAudioManager : AssistantAudioManagerInterface
{
    private int count;

    
    public AudioSource Intro;
    
    public AudioSource Walking_daquestaparte;
    
    public AudioSource Walking_seguimi;
    
    public AudioSource Walking_vieniconme;
    
    public AudioSource Pointing_carta;
    
    public AudioSource Pointing_plastica;
    
    public AudioSource Pointing_vetro;
    
    public AudioSource Pointing_cartabin;
    
    public AudioSource Pointing_plasticabin;
    
    public AudioSource Pointing_vetrobin;
    
    public AudioSource Jumping_benfatto;
    
    public AudioSource Jumping_eccellente;
    
    public AudioSource ShakingHeadNo_nonono;
    
    public AudioSource ShakingHeadNo_riprova;


    public override void PlayShakingHeadNo()
    {
        System.Random rnd = new System.Random();

        switch (rnd.Next(0, 1))
        {
            case 0:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(ShakingHeadNo_nonono);
                ShakingHeadNo_nonono.Play();
                break;
            case 1:
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(ShakingHeadNo_riprova);
                ShakingHeadNo_riprova.Play();
                break;
        }
        count = 0;
    }

    public override void PlayWalking()
    {
        GameObject target = VirtualAssistantManager.Instance.targetObject.gameObject;

        if (count % 5 == 0 && Vector3.Distance(VirtualAssistantManager.Instance.transform.position, target.transform.position) > 0.2f)
        {
            System.Random rnd = new System.Random();

            switch (rnd.Next(0, 3))
            {
                case 0:
                    //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Walking_vieniconme);
                    Walking_vieniconme.Play();
                    break;
                case 1:
                    //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Walking_seguimi);
                    Walking_seguimi.Play();
                    break;
                case 2:
                    //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Walking_daquestaparte);
                    Walking_daquestaparte.Play();
                    break;
            }
            count++;
        }
    }


    public override void PlayIntro()
    {
        //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Intro);
        Intro.Play();
        count = 0;
    }

    public override void PlayJump()
    {
        if (count == 0)
        {
            System.Random rnd = new System.Random();

            switch (rnd.Next(0, 1))
            {
                case 0:
                    //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Jumping_benfatto);
                    Jumping_benfatto.Play();
                    break;
                case 1:
                    //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Jumping_eccellente);
                    Jumping_eccellente.Play();
                    break;
            }
            count++;
        }
    }

    public override void PlayPointing()
    {
        GameObject target = VirtualAssistantManager.Instance.targetObject.gameObject;

        if (target.name.Contains("Bin"))
        {
            if (target.tag == "Paper")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_cartabin);
                Pointing_cartabin.Play();
            if (target.tag == "Plastic")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_plasticabin);
                Pointing_plasticabin.Play();
            if (target.tag == "Glass")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_vetrobin);
                Pointing_vetrobin.Play();
        }
        else
        {
            if (target.tag == "Paper")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_carta);
                Pointing_carta.Play();
            if (target.tag == "Plastic")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_plastica);
                Pointing_plastica.Play();
            if (target.tag == "Glass")
                //TaskManager.Instance.GetComponent<UAudioManager>().PlayEvent(Pointing_vetro);
                Pointing_vetro.Play();
        }
        count = 0;
    }

}