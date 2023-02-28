using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField]
    float fadeDuration = 1f;
    [SerializeField]
    bool playerExit;
    [SerializeField]
    bool playerCaught;
    [SerializeField]
    float timer;
    [SerializeField]
    float imageDuration = 1f;

    [Header("Canvas")]
    [SerializeField]
    CanvasGroup playerWon;
    [SerializeField]
    CanvasGroup playerLost;

    [Header("Player")]
    [SerializeField]
    GameObject player;

    void Update()
    {
        if (playerExit == true) 
        {
            EndLevel(playerWon, false);
        }
        else if (playerCaught == true) 
        { 
            EndLevel(playerLost, true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerExit= true;
        }
    }

    public void CaughtPlayer()
    {
        playerCaught = true;
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        timer += Time.deltaTime;

        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + imageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
