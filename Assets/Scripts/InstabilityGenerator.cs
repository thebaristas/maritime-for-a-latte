using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstabilityGenerator : MonoBehaviour
{
  [Header("Rocking Settings")]
  public float maxRockingOffset = 4;
  public float maxRockingVelocity = 10;
  public float rockingIntervalMin = 2;
  public float rockingIntervalMax = 5;

  [Header("Shaking Settings")]
  public float shakingIntervalMin = 2;
  public float shakingIntervalMax = 5;
  public float shakingDuration = 0.5f;
  public float shakingFrequency = 100;
  public float shakingAmplitude = 0.1f;

  Vector2 m_rockingTarget;
  float m_rockingInterval;
  float m_rockingTimer;
  Vector2 m_rockingVelocity;

  bool m_isShaking;
  float m_shakingTimer;

  void Start()
  {
    m_rockingTimer = m_rockingInterval = getRockingInterval();
    m_rockingTarget = getRockingTarget();

    m_isShaking = false;
    m_shakingTimer = getShakingInterval();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = getShakingOffset() + Vector2.SmoothDamp(transform.position, m_rockingTarget, ref m_rockingVelocity, m_rockingInterval);

    m_rockingTimer -= Time.deltaTime;
    m_shakingTimer -= Time.deltaTime;

    if (m_rockingTimer <= 0)
    {
      m_rockingTimer = m_rockingInterval = getRockingInterval();
      m_rockingTarget = getRockingTarget();
      Debug.Log(m_rockingInterval);
      Debug.Log(m_rockingTarget);
    }

    if (m_shakingTimer <= 0)
    {
      if (m_isShaking)
      {
        // Stop shaking
        m_isShaking = false;
        m_shakingTimer = getShakingInterval();
        GetComponent<Rigidbody2D>().velocity = m_rockingVelocity;
      }
      else
      {
        m_isShaking = true;
        m_shakingTimer = shakingDuration;
      }
    }
  }

  float getShakingInterval()
  {
    return Random.Range(shakingIntervalMin, shakingIntervalMax);
  }

  Vector2 getShakingOffset()
  {
    if (!m_isShaking) return Vector2.zero;
    var shakingOffset = shakingAmplitude * new Vector2(Mathf.Sin(2 * Mathf.PI * shakingFrequency * Time.time), 0);
    return shakingOffset;
  }

  Vector2 getRockingTarget()
  {
    return new Vector2(Random.Range(-maxRockingOffset, maxRockingOffset), 0);
  }

  float getRockingInterval()
  {
    return Random.Range(rockingIntervalMin, rockingIntervalMax);
  }
}
