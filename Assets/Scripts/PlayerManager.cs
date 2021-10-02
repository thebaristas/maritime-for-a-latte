using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public float smoothTime = 0f;

  Vector3 m_handVelocity;

  void Start()
  {
    transform.position = getMousePositionWorld();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.SmoothDamp(transform.position, getMousePositionWorld(), ref m_handVelocity, smoothTime);
  }

  Vector2 getMousePositionWorld()
  {
    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return pos;
  }
}
