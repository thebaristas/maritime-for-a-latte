using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings
{
  public float gameDuration = 120f;
  public float accuracyThreshold = 0.6f;
  public int milknessGridSize = 32;
  public float dropCooldownSeconds = 0.017f; // How often the latte data is updated
  public byte dropIntensity = 15; // How intense is each drop
  public int dropSizeFactor = 3; // A scalar controlling the maximum size of each drop
  public float latteBasePrice = 2.49f; // The price of a latte in pounds
  public float serveCooldownSeconds = 2f;
}

public enum GameState
{
  Menu,
  Playing,
  Pause,
  GameOver
}

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public GameSettings gameSettings;
  public PlayerManager playerManager;
  public LatteRenderer latteRenderer;
  public ShapeManager shapeManager;
  public UIManager uIManager;
  public float baseProfit { get; private set; } = 0f;
  public float tips { get; private set; } = 0f;
  public float remainingTime { get; private set; } = 120f;
  public bool isPlaying { get => gameState == GameState.Playing; }
  public GameState gameState { get; private set; } = GameState.Menu;
  public string[] failServeSounds = new string[]{ "angry-1", "angry-2", "angry-3" };
  private Vector2 m_scale = new Vector2(1f, 1f);
  private byte[,] m_milknessGrid;
  private float m_nextDropTimestamp = 0f;
  private float m_servingTimer;
  private bool m_playedTickSound;

  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    m_milknessGrid = new byte[gameSettings.milknessGridSize, gameSettings.milknessGridSize];
    playerManager.dropMilk = DropMilk;
    playerManager.completeCoffee = CompleteCoffee;
    playerManager.pauseGame = PauseGame;
    m_scale = transform.localScale;
    m_nextDropTimestamp = Time.time;
    AudioManager.instance.Play("boat-waves");
    AudioManager.instance.Play("seagulls");
    AudioManager.instance.Play("music-menu");
    uIManager.UpdateDisplay();
    uIManager.DisplayScore(baseProfit, tips);
    m_servingTimer = gameSettings.serveCooldownSeconds;
    m_playedTickSound = false;
    Application.targetFrameRate = 1000;
  }

  // Update is called once per frame
  void Update()
  {
    if (isPlaying)
    {
      if (remainingTime <= 0f)
      {
        FinishGameSession();
      }
      else
      {
        if (remainingTime < 10f && !m_playedTickSound)
        {
          m_playedTickSound = true;
          AudioManager.instance.Play("tick-tock");
        }
        remainingTime -= Time.deltaTime;
        m_servingTimer -= Time.deltaTime;
        uIManager.DisplayTime(remainingTime);
      }
    }
  }

  public void Play()
  {
    switch (gameState)
    {
      case GameState.Menu:
      case GameState.GameOver:
        StartGameSession();
        break;
      case GameState.Pause:
        Resume();
        break;
      default:
        break;
    }
  }

  public void StartGameSession()
  {
    AudioManager.instance.Stop("music-menu");
    AudioManager.instance.Play("music-game");
    remainingTime = gameSettings.gameDuration;
    baseProfit = 0f;
    tips = 0f;
    uIManager.DisplayScore(baseProfit, tips);
    InitialiseCoffee();
    gameState = GameState.Playing;
    uIManager.UpdateDisplay();
    m_playedTickSound = false;
    AudioManager.instance.Play("bell");
  }

  public void PauseGame()
  {
    AudioManager.instance.Pause("music-game");
    gameState = GameState.Pause;
    uIManager.UpdateDisplay();
  }

  public void Resume()
  {
    AudioManager.instance.Play("music-game");
    gameState = GameState.Playing;
    uIManager.UpdateDisplay();
  }

  public void FinishGameSession()
  {
    gameState = GameState.GameOver;
    AudioManager.instance.Play("bell");
    uIManager.UpdateDisplay();
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void DropMilk()
  {
    if (m_nextDropTimestamp <= Time.time)
    {
      Vector2 pos = playerManager.transform.position - transform.position;
      int x = Mathf.FloorToInt(pos.x * gameSettings.milknessGridSize / m_scale.x);
      int y = Mathf.FloorToInt(pos.y * gameSettings.milknessGridSize / m_scale.y);
      int maxX, maxY;
      maxX = maxY = gameSettings.milknessGridSize - 1;

      if (x >= 0 && x < gameSettings.milknessGridSize && y >= 0 && y < gameSettings.milknessGridSize)
      {
        int newMilkness = m_milknessGrid[x, y] + gameSettings.dropIntensity;
        m_milknessGrid[x, y] = (byte) Mathf.Min(newMilkness, byte.MaxValue);
        latteRenderer.DrawDroplet(x, y, maxX, maxY, m_milknessGrid[x, y]);
      }

      m_nextDropTimestamp = Time.time + gameSettings.dropCooldownSeconds;
    }
  }

  public void CompleteCoffee()
  {
    if (m_servingTimer <= 0)
    {
      m_servingTimer = gameSettings.serveCooldownSeconds;
      AudioManager.instance.Play("slide-cup");
      UpdateProfits();
      shapeManager.ChangeSpriteRandomly();
      InitialiseCoffee();
    }
  }

  private float ComputeAccuracy()
  {
    var shapeOpacityArray = shapeManager.GetOpacityArray();
    var latteRendererOpacityArray = latteRenderer.GetOpacityArray();
    int success = 0;
    int total = 0;
    for (int i = 0; i < Mathf.Min(shapeOpacityArray.Length, latteRendererOpacityArray.Length); i++)
    {
      var shapeOpacity = shapeOpacityArray[i];
      var latteOpacity = latteRendererOpacityArray[i];
      if (shapeOpacity > 0 || latteOpacity > 0)
      {
        if (Mathf.Abs(shapeOpacity - latteOpacity) < (byte.MaxValue / 2))
        {
          success += 1;
        }
        total += 1;
      }
    }
    return Mathf.Sqrt((float)success / (float)total); // sqrt to help the player :D
  }

  private float ComputeTip(float accuracy)
  {
    return Mathf.Pow(accuracy * 2, 4) / 6;
  }

  private void UpdateProfits()
  {
    float accuracy = ComputeAccuracy();
    if (accuracy >= gameSettings.accuracyThreshold)
    {
      float profitIncrement = gameSettings.latteBasePrice;
      float tipIncrement = ComputeTip(accuracy);
      baseProfit += profitIncrement;
      tips += tipIncrement;
      AudioManager.instance.Play("coins");
      uIManager.DropProfit(profitIncrement, tipIncrement);
      uIManager.DisplayScore(baseProfit, tips);
    }
    else
    {
        AudioManager.instance.PlayRandom(failServeSounds);
    }
  }

  private void InitialiseCoffee()
  {
    m_milknessGrid = new byte[gameSettings.milknessGridSize, gameSettings.milknessGridSize];
    latteRenderer.ClearTexture();
  }
}
