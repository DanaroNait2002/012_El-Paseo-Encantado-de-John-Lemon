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

    [Header("Modificación")]
    [SerializeField]
    float timerInitial = 2f;
    [SerializeField]
    float timer;
    [SerializeField]
    AudioSource playerSaw;
    [SerializeField]
    Canvas image;

    [Header("Game Ending")]
    [SerializeField]
    GameEnding gameEnding;

    private void Start()
    {
        timer = timerInitial;

        playerSaw = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange)
        {
            timer -= Time.deltaTime;

            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit)) 
            {
                if (raycastHit.collider.transform == player)
                { 
                    image.gameObject.SetActive(true);

                    if (timer <= 0f)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
        }

        if (!playerInRange)
        {
            image.gameObject.SetActive(false);

            timer = timerInitial;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            playerInRange = true;

            playerSaw.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInRange = false;
        }
    }
}
