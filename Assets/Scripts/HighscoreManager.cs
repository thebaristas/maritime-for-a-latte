using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
  public readonly int highscoreCount = 5;

  public float[] highscores { get; private set; }

  void Awake()
  {
    LoadHighscores();
  }

  void OnDestroy()
  {
    SaveHighscores();
  }

  public void AddScore(float score)
  {
    // Highscores array in sorted in decreasing order, so we search for the
    // first item that is smaller than our new score;
    if (highscores[highscoreCount - 1] >= score)
    {
      // All highscores are higher than the new score, so we can ignore it.
      return;
    }

    for (int i = 0; i < highscoreCount; ++i)
    {
      if (score > highscores[i])
      {
        // swap highscore with current score
        var tmp = highscores[i];
        highscores[i] = score;
        score = tmp;
      }
    }
  }

  private string GetSavePath()
  {
    return Application.persistentDataPath + "/highscores.bin";
  }

  private void LoadHighscores()
  {
    string savePath = GetSavePath();
    if (File.Exists(savePath))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(GetSavePath(), FileMode.Open);

      highscores = formatter.Deserialize(stream) as float[];
    }
    else
    {
      Debug.LogError("Save file not found: " + savePath);
      highscores = new float[highscoreCount];
    }
  }

  private void SaveHighscores()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(GetSavePath(), FileMode.Create);

    formatter.Serialize(stream, highscores);
    stream.Close();
  }
}
