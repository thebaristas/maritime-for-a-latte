using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationAmplitude = 10f;
    public float rotationPeriod = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,rotationAmplitude * Mathf.Sin(2 * Mathf.PI * Time.time / rotationPeriod)));
    }
}
