using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEnergyFromWorld : MonoBehaviour
{
    public float timeUntilEnd;
    public bool timerOn = false;
    public GameObject hero; 
      

    void Start()
    {
        hero = GameObject.Find("Third Person Player/Particle Transfer Manager");
        ParticleTranferState ptsScript = hero.GetComponentInChildren<ParticleTranferState>();
    }
    void Update()
    {
       if(timerOn)
        {
            if(timeUntilEnd > 0)
            {
                timeUntilEnd -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time is Up!");
                timeUntilEnd = 0;
                timerOn = false;
                ParticleTranferState ptsScript = hero.gameObject.GetComponentInChildren<ParticleTranferState>();
                if (ptsScript)
                {
                    ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StopTransfer);
                }
                else
                {
                    Debug.LogWarning("ParticleTransferStopState not found on player");
                }
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Collided with "+ other.gameObject.name);
        ParticleTranferState ptsScript = other.gameObject.GetComponentInChildren<ParticleTranferState>();
        if(ptsScript)
        {
            ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StartTransfer);
            timerOn = true;
        }
        else
        {
            Debug.LogWarning("ParticleTransferState not found on player");
        }
    }
}
