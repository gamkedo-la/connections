using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEnergyFromWorld : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Collided with "+ other.gameObject.name);
        ParticleTranferState ptsScript = other.gameObject.GetComponentInChildren<ParticleTranferState>();
        if(ptsScript)
        {
            ptsScript.ChangeStateTo(ParticleTranferState.TransferMode.StartTransfer);
        }
        else
        {
            Debug.LogWarning("ParticleTransferState not found on player");
        }
    }
}
