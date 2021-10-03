using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public float accuracyThreshold = 0.4f;
    public int milknessGridSize = 32;
    public float dropCooldownSeconds = 0.001f; // How often the latte data is updated
    public byte dropIntensity = 3; // How intense is each drop
    public int dropSizeFactor = 3; // A scalar controlling the maximum size of each drop
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameSettings gameSettings;
    public PlayerManager playerManager;
    public LatteRenderer latteRenderer;
    public ShapeManager shapeManager;
    public int score { get; private set; } = 0;
    public float latestAccuracy { get; private set; } = 0f;
    public float remainingTime { get; private set; } = 60f;
    public bool isPlaying { get; private set; } = false;
    private Vector2 m_scale = new Vector2(1f,1f);
    private byte[,] m_milknessGrid;
    private float m_nextDropTimestamp = 0f;

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
        m_scale = transform.localScale;
        m_nextDropTimestamp = Time.time;
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime <= 0f)
        {
            isPlaying = false;
        }
        if (isPlaying)
        {
            latteRenderer.RenderLatte(m_milknessGrid);
            remainingTime -= Time.deltaTime;
        }
    }

    public void DropMilk()
    {
        if (m_nextDropTimestamp <= Time.time)
        {
            Vector2 pos = playerManager.transform.position - transform.position;
            int x = Mathf.FloorToInt(pos.x * gameSettings.milknessGridSize / m_scale.x);
            int y = Mathf.FloorToInt(pos.y * gameSettings.milknessGridSize / m_scale.y);

            if (x >= 0 && x < gameSettings.milknessGridSize && y >= 0 && y < gameSettings.milknessGridSize)
            {
                int newMilkness = m_milknessGrid[x, y] + gameSettings.dropIntensity;
                m_milknessGrid[x, y] = (byte) Mathf.Min(newMilkness, byte.MaxValue);
            }

            m_nextDropTimestamp = Time.time + gameSettings.dropCooldownSeconds;
        }
    }

    public void CompleteCoffee()
    {
        UpdateScore();
        shapeManager.ChangeSpriteRandomly();
        ReinitialiseCoffee();
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
                if (Mathf.Abs(shapeOpacity - latteOpacity) < (byte.MaxValue/2))
                {
                    success += 1;
                }
                total += 1;
            }
        }
        return (float)success/(float)total;
    }

    private void UpdateScore()
    {
        latestAccuracy = ComputeAccuracy();
        Debug.Log(latestAccuracy);
        if (latestAccuracy >= gameSettings.accuracyThreshold)
        {
            score += 1;
        }
        Debug.Log(score);
    }

    private void ReinitialiseCoffee()
    {
        m_milknessGrid = new byte[gameSettings.milknessGridSize, gameSettings.milknessGridSize];
        latteRenderer.ClearTexture();
    }
}
