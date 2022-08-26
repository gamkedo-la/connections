using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsManLives : MonoBehaviour
{
    public GameObject DyingSwordsman;
    public GameObject Sword;
    public GameObject Swordsman;

    void Start()
    {
        Sword.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Sword.SetActive(true);
            DyingSwordsman.SetActive(false);
            Swordsman.SetActive(true);
        }
    }
}
