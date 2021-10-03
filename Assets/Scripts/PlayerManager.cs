using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Fire();

public class PlayerManager : MonoBehaviour
{
    public Fire fire; // set in GameManager
    public float positionSmoothTime = 0f;
    Vector3 m_handVelocity;
    
    void Start()
    {
        transform.position = getMousePositionWorld();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = getMousePositionWorld();
        if (Input.GetButton("Fire1") && fire != null)
        {
            fire();
        }
    }

    Vector2 getMousePositionWorld()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return pos;
    }
}
