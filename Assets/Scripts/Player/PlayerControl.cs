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
    private float swingPercentComplete;
    private float chargePercentComplete;
    private float resetPercentComplete;
    public float chargeSpeed;
    public float swingSpeed;
    public float resetSpeed;
    bool startAttackTimer;
    bool startChargeTimer;
    bool swingRunning;
    bool chargeRunning;
    bool resetRunning;

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        attackTimer = 0;
        chargeTimer = 0;
        swingPercentComplete = 0;
        chargePercentComplete = 0;
        resetPercentComplete = 0;
        startAttackTimer = false;
        startChargeTimer = false;
        swingRunning = false;
        chargeRunning = false;
        // startRotationQ = Quaternion.Euler(startRotation);
        //endRotationQ   = Quaternion.Euler(endRotation);
        resetRunning = false;
    }

    // Update is called once per frame
    void Update()
    {

        startPosition.rotation = sword.transform.rotation;
        endPosition.rotation = sword.transform.rotation;
        chargePosition.rotation = sword.transform.rotation;


        if (startAttackTimer == true)
        {
            attackTimer += Time.deltaTime;
        }

        if (startChargeTimer == true)
        {
            chargeTimer += Time.deltaTime;
        }

        if (chargeTimer >= 0.3 && swingRunning == false)
        {
            startChargeTimer = false;
            chargeTimer = 0;
            StartCoroutine(SwingSword());
        }



        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");

            startChargeTimer = true;

            if (chargeRunning == false)
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


        if (attackTimer >= 0.2 && resetRunning == false)
        {
            StartCoroutine(ResetSword());
            attackTimer = 0;
        }
    }

    IEnumerator Charge()
    {
        chargeRunning = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(startPosition.localPosition, chargePosition.localPosition, chargePercentComplete);
            //sword.transform.localRotation = Quaternion.Slerp(startPosition.transform.localRotation, chargePosition.localRotation, chargePercentComplete);
            chargePercentComplete += (Time.deltaTime / chargeTime * chargeSpeed);

            yield return null;

        } while (chargePercentComplete < 1.0);


        chargeRunning = false;
        chargePercentComplete = 0;

    }



    IEnumerator SwingSword()
    {
        swingRunning = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(chargePosition.localPosition, endPosition.localPosition, swingPercentComplete);
            //sword.transform.localRotation = Quaternion.Slerp(chargePosition.localRotation, endPosition.localRotation, swingPercentComplete);
            swingPercentComplete += (Time.deltaTime / swingTime * swingSpeed);


            yield return null;

        } while (swingPercentComplete < 1.0);

        swingPercentComplete = 0;
        swingRunning = false;
        startAttackTimer = true;
    }


    IEnumerator ResetSword()
    {
        resetRunning = true;

        do
        {
            sword.transform.localPosition = Vector3.Lerp(endPosition.localPosition, startPosition.localPosition, resetPercentComplete);
            //sword.transform.localRotation = Quaternion.Slerp(endPosition.localRotation, startPosition.localRotation, swingPercentComplete);
            resetPercentComplete += (Time.deltaTime / resetTime * resetSpeed);
            yield return null;

        } while (resetPercentComplete < 1.0);


        resetPercentComplete = 0;
        startAttackTimer = false;
        resetRunning = false;
    }


}
