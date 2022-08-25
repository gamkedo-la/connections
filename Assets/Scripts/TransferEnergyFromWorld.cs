using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TransferEnergyFromWorld : MonoBehaviour
{
    public float timeUntilEnd;
    public float timeUntilExplode;
    public bool timerOn = false;
    public bool explodeTimerOn = false;
    public GameObject hero;
    public GameObject yellowOrbitingMotes;
    public GameObject explosion;
    public VisualEffect explosionVFX;
    public int explosionforce;
    public VFXExposedProperty exposed;
    public string explosionVFXString;
    public bool ExplodeTimer;
    public float timeUntilBlastOff;


    void Start()
    {
        hero = GameObject.Find("Third Person Player/Particle Transfer Manager");
        ParticleTranferState ptsScript = hero.GetComponentInChildren<ParticleTranferState>();

        yellowOrbitingMotes = GameObject.Find("Third Person Player/Rising Animator/GFX/YellowOrbiting Motes");
        yellowOrbitingMotes.SetActive(false);

        explosion = GameObject.Find("Third Person Player/Explosion");
        explosion.SetActive(false);
        explosionVFX = explosion.GetComponent<VisualEffect>();
        explosionVFXString = "force";
        explosionVFX.SetInt(explosionVFXString, 1);
        //Debug.Log(explosionVFX);
    }
    void Update()
    {
        if (timerOn)
        {
            if (timeUntilEnd > 0)
            {
                timeUntilEnd -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time is Up!");
                timeUntilEnd = 0;
                timerOn = false;
                explodeTimerOn = true;
                ParticleTranferState ptsScript = hero.gameObject.GetComponentInChildren<ParticleTranferState>();
                if (ptsScript)
                {
                    ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StopTransfer);
                    yellowOrbitingMotes.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("ParticleTransferStopState not found on player");
                }
            }
        }
        if (explodeTimerOn)
        {
            explosion.SetActive(true);
            explosionVFX.SetInt(explosionVFXString, 3);
            ParticleTranferState ptsScript = hero.gameObject.GetComponentInChildren<ParticleTranferState>();
            ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StopTransfer);
            if (timeUntilExplode > 0)
            {
                timeUntilExplode -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time is Up for Explode!");
                ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.BlastGather);
                explosionVFX.SetInt(explosionVFXString, -4);
                timeUntilExplode = 0;
                timerOn = false;
                explodeTimerOn = false;
                ExplodeTimer = true;

                //ParticleTranferState ptsScript = hero.gameObject.GetComponentInChildren<ParticleTranferState>();
                if (ptsScript)
                {
                    ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.BlastExplode);
                    explosion.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("ParticleTransferBlastExplode not found on player");
                }
            }
        }
        if (ExplodeTimer)
        {
            ParticleTranferState ptsScript = hero.gameObject.GetComponentInChildren<ParticleTranferState>();
            if (timeUntilBlastOff > 0)
            {
                timeUntilBlastOff -= Time.deltaTime;
            }
            else
            {
                ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.BlastExplode);
                if (ptsScript)
                {
                    ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.BlastExplode);
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        ParticleTranferState ptsScript = other.gameObject.GetComponentInChildren<ParticleTranferState>();
        if (ptsScript)
        {
            ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StartTransfer);
            timerOn = true;
        }
        else if (ptsScript)
        {
            ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.BlastGather);
            explodeTimerOn = true;
        }
        else
        {
            Debug.LogWarning("ParticleTransferState not found on player");
        }
    }


}
