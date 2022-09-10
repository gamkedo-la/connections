using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllAudio : MonoBehaviour
{
    private AudioSource[] allAudioSources;
    public GameObject Music;
    public AudioSource newMusic;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            newMusic = Music.GetComponent<AudioSource>();
            newMusic.Play(0);
        }
    }
}
