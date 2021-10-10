using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISubmitButtonManager : MonoBehaviour
{
    private Button m_submitButton;
    // Start is called before the first frame update
    void Start()
    {
        m_submitButton = GetComponent<Button>();
        if (m_submitButton != null)
        {
            m_submitButton.onClick.AddListener(OnSubmitClick);
        }
    }

    public void OnSubmitClick()
    {
        GameManager.instance.CompleteCoffee();
    }
}
