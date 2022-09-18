using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsManLives : MonoBehaviour
{
    public GameObject DyingSwordsman;
    public GameObject Sword;
    public GameObject Swordsman;
    public GameObject PlayerBlue;
    public GameObject Ambient_Loc;
    public GameObject BlueSwordsman_Loc;
    public AudioSource Ambient;
    public AudioSource BlueSwordsman;
    bool Ambient_Play;
    bool BlueSwordsman_Play;
    private AudioSource[] allAudioSources;


    void Start()
    {
        Sword.SetActive(false);
        PlayerBlue.SetActive(false);

        Ambient_Loc = GameObject.Find("Audio/Ambient Audio");
        Ambient_Loc.GetComponent<AudioSource>();
        Ambient_Play = false;

        BlueSwordsman_Loc = GameObject.Find("Audio/BlueSwordsman Audio");
        BlueSwordsman_Loc.GetComponent<AudioSource>();
        BlueSwordsman_Play = false;        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            Sword.SetActive(true); 
            DyingSwordsman.SetActive(false);
            Swordsman.SetActive(true);
            PlayerBlue.SetActive(true);
            Ambient_Play = true;
            if(Ambient_Play == true)
            {
                Ambient.Play(0);
                Ambient.Stop();
                BlueSwordsman_Play = true;
                BlueSwordsman.Play(0);
            }

        }
    }
}
