using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Fire();

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Fire fire;

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Mouse X");
        float verticalMove = Input.GetAxis("Mouse Y");

        transform.Translate(new Vector2(moveSpeed * horizontalMove, moveSpeed * verticalMove), Space.Self); 

        if (Input.GetButtonDown("Fire1") && fire != null)
        {
            fire();
        }

    }
}
