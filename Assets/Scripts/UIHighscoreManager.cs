using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighscoreManager : MonoBehaviour
{
  public RectTransform parent;
  public RectTransform topScorePosition;
  public RectTransform secondScorePosition;
  public GameObject highscorePrefab;

  HighscoreManager highscoreManager;
  Vector2 offset;

  void Start()
  {
    highscoreManager = GameManager.instance.GetComponent<HighscoreManager>();
    offset = secondScorePosition.position - topScorePosition.position;
  }

  public void UpdateDisplay()
  {
    for (int i = 0; i < highscoreManager.highscores.Length; ++i)
    {
      var go = Instantiate(highscorePrefab, GetHighscoreItemPosition(i), topScorePosition.rotation, parent);
      go.GetComponent<Text>().text = string.Format("{0}. Name . . £{1:0.00}", i + 1, highscoreManager.highscores[i]);
    }
  }

  private Vector2 GetHighscoreItemPosition(int index)
  {
    return (Vector2)topScorePosition.position + index * offset;
  }
}
