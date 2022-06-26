using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTranferState : MonoBehaviour
{
    public enum TransferMode { Off, StartTransfer, OrbitingMotesOn, BlastActive, LingeringParticles };
    private TransferMode currentMode = TransferMode.Off;
    public TransferMode nextMode = TransferMode.Off;
    // Start is called before the first frame update
    void Start()
    {
        
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