using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultsManager : MonoBehaviour
{
    public Text baseProfitText;
    public Text tipsText;
    public Text totalProfitText;

    public void UpdateDisplay()
    {
        DisplayScore(GameManager.instance.baseProfit, GameManager.instance.tips);
    }

    public void DisplayScore(float profit, float tips)
    {
        baseProfitText.text = string.Format("Profit . . . . £{0:0.00}", profit);
        tipsText.text = string.Format("Tips . . . . . . £{0:0.00}", tips);
        totalProfitText.text = string.Format("Total . . . . . £{0:0.00}", profit + tips);
    }
}
