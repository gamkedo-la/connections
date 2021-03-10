using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ActivateTrailwhenMoving : MonoBehaviour
{
    public Vector3 playerPosition;

    public GameObject trails;
    public VFXSpawnerState trailSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //trailSpawn = trails.GetComponent<VFXSpawnerState>
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;

        if(playerPosition == transform.position)
        {
            //if player position does not equal transform.position then SPAWN ON
            //trails.Play();
        }

        else
        {
            //else Spawn is off
        }
    }
}
