using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oberver : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Transform player;
    [SerializeField]
    bool playerInRange;

    [Header("Game Ending")]
    [SerializeField]
    GameEnding gameEnding;

    void Update()
    {
        if (playerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit)) 
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer(); 
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }
}
