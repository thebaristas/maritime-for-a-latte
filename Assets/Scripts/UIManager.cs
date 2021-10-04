using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text accuracyText;
    public Text timeText;
    public Text gameOverText;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            DisplayScore(GameManager.instance.score, GameManager.instance.latestAccuracy);
            DisplayTime(GameManager.instance.remainingTime);
        }
        else
        {
            DisplayGameOver();
        }
    }

    public void DisplayScore(int score, float accuracy)
    {
        scoreText.text = string.Format("£{0}.00", score);
        accuracyText.text = string.Format("Latest accuracy: {0}%", Mathf.Floor(accuracy * 100));
    }

    public void DisplayTime(float time)
    {
        timeText.text = string.Format("{0}s", Mathf.CeilToInt(time));
    }

    public void DisplayGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = string.Format("Time's up! Successful lattes: {0}", GameManager.instance.score);
    }
}
