using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.CompleteLevel();
        }
    }
}
