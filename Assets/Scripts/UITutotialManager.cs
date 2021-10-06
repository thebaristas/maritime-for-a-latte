using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutotialManager : MonoBehaviour
{
    private Text m_tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        m_tutorialText = GetComponent<Text>();
        var tutorialTextTemplate = @"You are selling latte to passengers on a boat. Your goal is to maximise profit.

1. Pour milk {0}
2. Draw the art shown on the coffee
3. Serve the latte {1}

The more accurate your art, the more the passengers will tip!";
        # if UNITY_IOS || UNITY_ANDROID
            m_tutorialText.text = string.Format(tutorialTextTemplate, "by touching the screen", "by pressing \"Sell\" button");
        # else
            m_tutorialText.text = string.Format(tutorialTextTemplate, "with *Click*", "with *Space*");
        # endif

    }
}
