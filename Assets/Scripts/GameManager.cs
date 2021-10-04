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
  public float dropCooldownSeconds = 0.001f; // How often the latte data is updated
  public byte dropIntensity = 4; // How intense is each drop
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
  private Vector2 m_scale = new Vector2(1f, 1f);
  private byte[,] m_milknessGrid;
  private float m_nextDropTimestamp = 0f;
  private float m_servingTimer;
  private bool m_tickPlayed = false;


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
        if (remainingTime < 10f)
        {
          if (!m_tickPlayed)
          {
            m_tickPlayed = true;
            AudioManager.instance.Play("tick-tock");
          }
        }
        latteRenderer.RenderLatte(m_milknessGrid);
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
  case GameState.Pau
    Resume();
    break;
  default:
    break;
}
=======
      case GameState.Menu:
      case GameState.GameOver:
        StartGameSession();
        break;
      case GameState.Pause:
        Resume();
        break;
      default:
        break;
>>>>>>> 98d5554 (feat: more sounds)
  }
}

<<<<<<< HEAD

{

dioManager.instance.Pause("mus
meState = GameState.Pause;
}


{

dioManager.instance.Play("music-
meState = GameState.Playing;
}


{

meState = GameState.GameOver
}

=======
  public void StartGameSession()
  {
    AudioManager.instance.Stop("music-menu");
    AudioManager.instance.Play("music-game");
    m_tickPlayed = false;
    remainingTime = gameSettings.gameDuration;
    baseProfit = 0f;
    tips = 0f;
    uIManager.DisplayScore(baseProfit, tips);
    ReinitialiseCoffee();
    gameState = GameState.Playing;
    uIManager.DisplayOverlay(false);
  }

  public void PauseGame()
  {
    AudioManager.instance.Pause("music-game");
    gameState = GameState.Pause;
    uIManager.DisplayOverlay(true);
  }

  public void Resume()
  {
    AudioManager.instance.Play("music-game");
    gameState = GameState.Playing;
    uIManager.DisplayOverlay(false);
  }
>>>>>>> 98d5554 (feat: more sounds)
  {
{
    AudioManager.instance.Play("s
    UpdateProfits();
    shapeManager.ChangeSpriteRandomly
    ReinitialiseCoffee();
}


{
{
}

  int success = 0;
{
  for (int i = 0; i < Mathf.Min(shapeOp
  {
    var shapeOpacity = shapeOpacityArray[i];
    var latteOpacity = latteRendererOpacityArray[i];
    if (shapeOpacity > 0 || latteOpacity > 0)

      if (Mathf.Abs(shapeOpacity - latteOpacity) < (byte.MaxValue / 2))
    {
        success += 1;
      }
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
    ReinitialiseCoffee();
  }
}

    baseProfit += profitIncreme
{
    AudioManager.instance.Play("coins");
    uIManager.DropProfit(profitIncrement, tipIncrement);
    uIManager.Disp
  }
  else
  {
    int angrySoundIndex = UnityEngine.Random
    string angrySoundName = "angry-" + angrySoundInd
    AudioManager.instance.Play(angrySoundName
    {
}
      {
private void Reinitia
      }
      total += 1;
    }
  }

