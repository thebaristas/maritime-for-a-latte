using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text baseProfitText;
    public Text tipsText;
    public Text totalProfitText;
    public Text dropText;
    public Text timeText;
    public GameObject profit;
    public GameObject time;
    public GameObject overlayBackground;
    public UIOverlayManager overlay;
    public RectTransform profitImageTransform;
    public float dropSpeed = 3f;

    private readonly Color32 c_lowTimeColour = new Color32(200, 50, 35, 255);

    // Update is called once per frame
    void Update()
    {
        dropText.transform.position = Vector2.MoveTowards(dropText.transform.position, profitImageTransform.position, dropSpeed * Time.deltaTime);
    }

     public void UpdateDisplay()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.Playing:
                DisplayInGameUI(true);
                DisplayOverlay(false);
                break;
            case GameState.Menu:
            case GameState.GameOver:
                DisplayInGameUI(false);
                DisplayOverlay(true);
                break;
            case GameState.Pause:
                DisplayInGameUI(true);
                DisplayOverlay(true);
                break;
            default:
                break;
        }
    }

    public void DropProfit(float profitIncrement, float tipIncrement)
    {
        dropText.transform.position -= new Vector3(0, Camera.main.orthographicSize * 2 / 3, 0);
        dropText.text = string.Format("+ £{0:0.00}", profitIncrement + tipIncrement);
    }

    public void DisplayScore(float profit, float tips)
    {
        baseProfitText.text = string.Format("£{0:0.00}", profit);
        tipsText.text = string.Format("+ £{0:0.00}", tips);
        totalProfitText.text = string.Format("£{0:0.00}", profit + tips);
    }

    public void DisplayTime(float time)
    {
        if (time <= 10)
        {
            timeText.color = c_lowTimeColour;
        }
        else
        {
            timeText.color = Color.black;
        }
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(time);
        timeText.text = timeSpan.ToString(@"mm\:ss");
    }

    public void DisplayOverlay(bool isDisplayed)
    {
        overlayBackground.SetActive(isDisplayed);
        overlay.gameObject.SetActive(isDisplayed);
        Cursor.visible = isDisplayed;
        if (isDisplayed)
        {
            overlay.UpdateDisplay();
        }
    }

    public void DisplayInGameUI(bool isDisplayed)
    {
        if (profit != null && time != null)
        {
            profit.SetActive(isDisplayed);
            time.SetActive(isDisplayed);
            dropText.gameObject.SetActive(isDisplayed);
        }
    }
}
