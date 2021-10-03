using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettings
{
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

    private readonly byte[,] c_dripKernel = new byte[1,1] { { 1 } };

    private Vector2 m_scale = new Vector2(1f,1f);
    private byte[,] m_milknessGrid;
    float m_nextDropTimestamp = 0f;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_milknessGrid = new byte[gameSettings.milknessGridSize, gameSettings.milknessGridSize];
        playerManager.fire = DropMilk;
        m_scale = transform.localScale;
        m_nextDropTimestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        latteRenderer.RenderLatte(m_milknessGrid);
    }

    public void DropMilk()
    {
        if (m_nextDropTimestamp <= Time.time)
        {
            Vector2 pos = playerManager.transform.position - transform.position;
            int gridX = Mathf.FloorToInt(pos.x * gameSettings.milknessGridSize / m_scale.x);
            int gridY = Mathf.FloorToInt(pos.y * gameSettings.milknessGridSize / m_scale.y);

            int kOffsetX = (c_dripKernel.GetLength(0) - 1) / 2;
            int kOffsetY = (c_dripKernel.GetLength(1) - 1) / 2;

            for (int kX = -kOffsetX; kX <= kOffsetX; kX++)
            {
                for (int kY = -kOffsetY; kY <= kOffsetY; kY++)
                {
                    int x = gridX + kX, y = gridY+ kY;
                    if (x >= 0 && x < gameSettings.milknessGridSize && y >= 0 && y < gameSettings.milknessGridSize)
                    {
                        int newValue = m_milknessGrid[x, y] + gameSettings.dropIntensity * c_dripKernel[kX + kOffsetX, kY + kOffsetY];
                        m_milknessGrid[x, y] = (byte) Mathf.Min(newValue, byte.MaxValue);
                    }
                }
            }
            m_nextDropTimestamp = Time.time + gameSettings.dropCooldownSeconds;
        }
    }
}
