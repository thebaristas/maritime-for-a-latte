using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonsManager : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    private Text m_playText;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(OnPlayClick);
        ExitButton.onClick.AddListener(OnExitClick);
        m_playText = PlayButton.GetComponentInChildren<Text>();
    }

    public void UpdateDisplay()
    {
        if (GameManager.instance.gameState == GameState.Pause)
        {
            m_playText.text = "Resume";
        }
        else
        {
            m_playText.text = "Play";
        }
    }

    public void OnPlayClick()
    {
        GameManager.instance.Play();
    }

    public void OnExitClick()
    {
        GameManager.instance.QuitGame();
    }

}
