using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayManager : MonoBehaviour
{
    public Text titleText;
    public GameObject tutorial;
    public UIResultsManager resultsManager;
    public UIButtonsManager buttonsManager;

    public void UpdateDisplay()
    {
        switch (GameManager.instance.gameState)
        {
            case GameState.GameOver:
                tutorial.gameObject.SetActive(false);
                resultsManager.gameObject.SetActive(true);
                resultsManager.UpdateDisplay();
                buttonsManager.UpdateDisplay();
                break;
            default:
                tutorial.gameObject.SetActive(true);
                resultsManager.gameObject.SetActive(false);
                buttonsManager.UpdateDisplay();
                break;
        }
    }
}
