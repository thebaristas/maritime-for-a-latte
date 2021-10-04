using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayManager : MonoBehaviour
{
    public Text TitleText;
    public UIResultsManager resultsManager;
    public UIButtonsManager buttonsManager;

    // Start is called before the first frame update
    void Start()
    {
        resultsManager.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.GameOver)
        {
            resultsManager.gameObject.SetActive(true);
        }
    }
}
