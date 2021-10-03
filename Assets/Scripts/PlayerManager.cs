using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Action();

public class PlayerManager : MonoBehaviour
{
  public Action dropMilk; // set in GameManager
  public Action completeCoffee; // set in GameManager

  Vector3 m_handVelocity;

  void Start()
  {
    transform.position = getMousePositionWorld();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = getMousePositionWorld();
    if (Input.GetButton("Fire1") && dropMilk != null)
    {
        dropMilk();
    }
    if (Input.GetButtonDown("Jump") && completeCoffee != null)
    {
        completeCoffee();
    }
  }

  Vector2 getMousePositionWorld()
  {
    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return pos;
  }
}
