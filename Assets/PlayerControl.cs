using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");
            Collider[] targets = Physics.OverlapSphere(transform.position + transform.forward * 1.0f, 1.5f, enemyMask);
            foreach (Collider eachTarget in targets)
            {
                Debug.Log("Hit: " + eachTarget.gameObject.name);
                EnemyDestroy eScript = eachTarget.GetComponent<EnemyDestroy>();
                eScript.SwordHit();
            }
        }
    }
}
