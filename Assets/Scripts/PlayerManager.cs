using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Action();

public class PlayerManager : MonoBehaviour
{
  public Action dropMilk; // set in GameManager
  public Action completeCoffee; // set in GameManager
  public Action pauseGame; // set in GameManager
  public Animator pouringAnimator;

  Vector3 m_handVelocity;

  void Start()
  {
    transform.position = getMousePositionWorld();
    Cursor.visible = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.instance.isPlaying)
    {
        transform.position = getMousePositionWorld();
        pouringAnimator.SetBool("isPouring", Input.GetButton("Fire1"));
        if (Input.GetButton("Fire1") && dropMilk != null && pouringAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pouring"))
        {
            dropMilk();
        }
        if (Input.GetButtonDown("Jump") && completeCoffee != null)
        {
            completeCoffee();
        }
        if (Input.GetButtonDown("Cancel") && pauseGame != null)
        {
            pauseGame();
        }
    }
  }

  Vector2 getMousePositionWorld()
  {
    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return pos;
  }
}
