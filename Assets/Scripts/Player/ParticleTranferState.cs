using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleTranferState : MonoBehaviour
{
    public enum TransferMode { Off, StartTransfer, StopTransfer, BlastGather, OrbitingMotesOn, BlastExplode, LingeringParticles };
    private TransferMode currentMode = TransferMode.Off;
    public TransferMode nextMode = TransferMode.Off;
    public VisualEffect worldTreeTransfer;

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
                    worldTreeTransfer.SendEvent("StartEffect");
                    break;
                case TransferMode.StopTransfer:
                    worldTreeTransfer.SendEvent("StopEffect");
                    break;
                //case TransferMode.OrbitingMotesOn:
                    //break;
                case TransferMode.BlastGather:
                    worldTreeTransfer.SendEvent("StartExplosion");
                    break;
                case TransferMode.BlastExplode:
                    worldTreeTransfer.SendEvent("StopExplosion");
                    break;
                case TransferMode.LingeringParticles:
                    break;
            }
            Debug.Log("Switching state from " + currentMode + " to " + nextMode);
            currentMode = nextMode;
        }
    }
}