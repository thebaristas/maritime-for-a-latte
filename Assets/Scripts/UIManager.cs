using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text baseProfitText;
    public Text tipsText;
    public Text totalProfitText;
    public Text timeText;
    public Text gameOverText;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            DisplayScore(GameManager.instance.baseProfit, GameManager.instance.tips);
            DisplayTime(GameManager.instance.remainingTime);
        }
        else
        {
            DisplayGameOver();
        }
    }

    public void DisplayScore(float profit, float tips)
    {
        baseProfitText.text = string.Format("£{0:0.00}", profit);
        tipsText.text = string.Format("+ £{0:0.00}", tips);
        totalProfitText.text = string.Format("£{0:0.00}", profit + tips);
    }

    public void DisplayTime(float time)
    {
        timeText.text = string.Format("{0}s", Mathf.CeilToInt(time));
    }

    public void DisplayGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = string.Format("Time's up! Profit: £{0:0.00}", GameManager.instance.baseProfit + GameManager.instance.tips);
    }
}
