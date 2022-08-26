using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsManLives : MonoBehaviour
{
    public GameObject DyingSwordsman;
    public GameObject Sword;
    public GameObject Swordsman;
    public GameObject PlayerBlue;
    public AudioSource Ambient;
    public AudioSource BlueSwordsman;
    bool Ambient_Play;
    bool BlueSwordsman_Play;

    void Start()
    {
        Sword.SetActive(false);
        PlayerBlue.SetActive(false);

        Ambient = GetComponent<AudioSource>();
        Ambient.Play(0);
        Ambient_Play = true;

        BlueSwordsman_Play = false;
        BlueSwordsman = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Sword.SetActive(true);
            DyingSwordsman.SetActive(false);
            Swordsman.SetActive(true);
            PlayerBlue.SetActive(true);
            Ambient_Play = false;
            if(Ambient_Play == false)
            {
                Ambient.Stop();
                BlueSwordsman_Play = true;
                BlueSwordsman.Play(0);
            }

        }
    }
}
