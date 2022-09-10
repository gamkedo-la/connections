using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public void SwordHit()
    {
        Destroy(gameObject);
    }
}
