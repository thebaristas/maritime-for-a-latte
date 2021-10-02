using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Mouse X");
        float verticalMove = Input.GetAxis("Mouse Y");

        transform.Translate(new Vector2(moveSpeed * horizontalMove, moveSpeed * verticalMove), Space.Self); 
    }
}
