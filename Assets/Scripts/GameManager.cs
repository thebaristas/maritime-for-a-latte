using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int c_milkGridSize = 1024;
    const float c_resolution = 1000f; // grid cells per World unit
    private Vector2 m_scale = new Vector2(1f,1f);
    private byte[,] m_milkGrid;
    float m_nextDropTimestamp = 0f;

    public PlayerManager playerManager;
    public float dropCooldownSeconds = 0.1f; // How often the latte data is updated
    public byte dropIntensity = 3; // How intense is each drop

    // Start is called before the first frame update
    void Start()
    {
        m_milkGrid = new byte[c_milkGridSize, c_milkGridSize];
        playerManager.fire = DropMilk;
        m_scale = transform.localScale;
        m_nextDropTimestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: trigger rendering
    }

    public void DropMilk() 
    {
        if (m_nextDropTimestamp <= Time.time)
        {
            Vector2 pos = playerManager.transform.position - transform.position;
            var x = Mathf.FloorToInt(pos.x * c_resolution / m_scale.x);
            var y = Mathf.FloorToInt(pos.y * c_resolution / m_scale.y);
            if (x >= 0 && x < c_milkGridSize && y >= 0 && y < c_milkGridSize)
            {
                if (m_milkGrid[x, y] < byte.MaxValue) 
                {
                    m_milkGrid[x, y] += dropIntensity;
                }
                m_nextDropTimestamp = Time.time + dropCooldownSeconds;
                Debug.Log(m_milkGrid[x, y]); // TODO: remove log once we have visual feedback
            }
            // TODO: deal with mouse out of bounds
        }
    }
}
