using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int c_milkGridSize = 1024;
    private byte[,] m_milkGrid;
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        m_milkGrid = new byte[c_milkGridSize, c_milkGridSize];
        playerManager.fire = PourMilk;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_milkGrid);
    }

    public void PourMilk() 
    {
        Vector2 pos = playerManager.transform.position - transform.position;
        var x = Mathf.FloorToInt(pos.x * 1000);
        var y = Mathf.FloorToInt(pos.y * 1000);
        if (x < c_milkGridSize && y < c_milkGridSize)
        {
            m_milkGrid[x, y] += 1;
        }
        // TODO: deal with mouse out of bounds
    }
}
