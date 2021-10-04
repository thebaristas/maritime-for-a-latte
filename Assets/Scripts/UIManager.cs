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
    public GameObject overlayBackground;
    public UIOverlayManager overlay;

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

    public void DisplayOverlay(bool isDisplayed)
    {
        overlayBackground.SetActive(isDisplayed);
        overlay.gameObject.SetActive(isDisplayed);
    }
}
