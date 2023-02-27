using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField]
    float fadeDuration = 1f;
    [SerializeField]
    bool playerExit;
    [SerializeField]
    float timer;
    [SerializeField]
    float imageDuration = 1f;

    [Header("Canvas")]
    [SerializeField]
    CanvasGroup playerWon;

    [Header("Player")]
    [SerializeField]
    GameObject player;

    void Update()
    {
        if (playerExit == true) 
        {
            EndLevel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerExit= true;
        }
    }

    void EndLevel()
    {
        timer += Time.deltaTime;

        playerWon.alpha = timer / fadeDuration;

        if (timer > fadeDuration + imageDuration)
        {
            Application.Quit();
        }
    }
}
