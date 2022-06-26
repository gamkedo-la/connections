using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTranferState : MonoBehaviour
{
    public enum TransferMode { Off, StartTransfer, OrbitingMotesOn, BlastActive, LingeringParticles };
    private TransferMode currentMode = TransferMode.Off;
    public TransferMode nextMode = TransferMode.Off;

    public void ChangeStateTo(TransferMode toState)
    {
        //if we need to, we can prevent incorrect transitions
        nextMode = toState;    
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMode != nextMode)
        {
            switch (nextMode)
            {
                case TransferMode.Off:
                    break;
                case TransferMode.StartTransfer:
                    break;
                case TransferMode.OrbitingMotesOn:
                    break;
                case TransferMode.BlastActive:
                    break;
                case TransferMode.LingeringParticles:
                    break;
            }
            Debug.Log("Switching state from " + currentMode + " to " + nextMode);
            currentMode = nextMode;
        }
    }
}