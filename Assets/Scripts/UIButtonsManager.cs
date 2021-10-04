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

    public void ConfigurePlay()
    {
        m_playText.text = "Play";
    }

    public void ConfigurePause()
    {
        m_playText.text = "Resume";
    }

    public void OnPlayClick()
    {
        GameManager.instance.StartGameSession();
    }

    public void OnExitClick()
    {
        GameManager.instance.QuitGame();
    }

}
