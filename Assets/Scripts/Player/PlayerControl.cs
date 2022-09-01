using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    LayerMask enemyMask;
    public GameObject sword;
    public Transform startPosition, endPosition, chargePosition;
    //public Vector3 startRotation, endRotation;
    // Quaternion startRotationQ, endRotationQ;
    public float swingTime;
    public float chargeTime;
    public float resetTime;
    private float attackTimer;
    private float chargeTimer;
    private float percentComplete;
    public float chargeSpeed;
    public float swingSpeed;
    public float resetSpeed;
    bool startAttackTimer;
    bool startChargeTimer;
    bool running;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        attackTimer = 0;
        chargeTimer = 0;
        percentComplete = 0;
        startAttackTimer = false;
        startChargeTimer = false;
        running = false;
       // startRotationQ = Quaternion.Euler(startRotation);
        //endRotationQ   = Quaternion.Euler(endRotation);
    }

    // Update is called once per frame
    void Update()
    {

       // startPosition.rotation = sword.transform.rotation;
        //endPosition.rotation = sword.transform.rotation;
        //chargePosition.rotation = sword.transform.rotation;


        if (startAttackTimer == true)
        {
            attackTimer += Time.deltaTime;
        }

        if (startChargeTimer == true)
        {
            chargeTimer += Time.deltaTime;
        }

        if (chargeTimer >= 0.3 && running == false)
        {
            startChargeTimer = false;
            chargeTimer = 0;
            StartCoroutine(SwingSword());
        }



        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");

            startChargeTimer = true;

            if (running == false)
            {
                StartCoroutine(Charge());
            }

            Collider[] targets = Physics.OverlapSphere(transform.position + transform.forward * 1.0f, 1.5f, enemyMask);
            foreach (Collider eachTarget in targets)
            {
                Debug.Log("Hit: " + eachTarget.gameObject.name);
                EnemyDestroy eScript = eachTarget.GetComponent<EnemyDestroy>();
                eScript.SwordHit();
            }
        }


        if (attackTimer >= 0.2 && running == false)
        {
            StartCoroutine(ResetSword());
            attackTimer = 0;
        }
    }

    IEnumerator Charge()
    {
        running = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(startPosition.localPosition, chargePosition.localPosition, percentComplete);
            sword.transform.localRotation = Quaternion.Slerp(startPosition.transform.localRotation, chargePosition.localRotation, percentComplete);
            percentComplete += (Time.deltaTime / chargeTime * chargeSpeed);

            yield return null;

        } while (percentComplete < 1.0);


        running = false;
        percentComplete = 0;

    }



    IEnumerator SwingSword()
    {
        running = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(chargePosition.localPosition, endPosition.localPosition, percentComplete);
            sword.transform.localRotation = Quaternion.Slerp(chargePosition.localRotation, endPosition.localRotation, percentComplete);
            percentComplete += (Time.deltaTime / swingTime * swingSpeed);


            yield return null;

        } while (percentComplete < 1.0);

        percentComplete = 0;
        running = false;
        startAttackTimer = true;
    }


    IEnumerator ResetSword()
    {
        running = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(endPosition.localPosition, startPosition.localPosition, percentComplete);
            sword.transform.localRotation = Quaternion.Slerp(endPosition.localRotation, startPosition.localRotation, percentComplete);
            percentComplete += (Time.deltaTime / resetTime * resetSpeed);
            yield return null;

        } while (percentComplete < 1.0);


        percentComplete = 0;
        startAttackTimer = false;
        running = false;
    }
}
