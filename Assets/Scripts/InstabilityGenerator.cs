using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstabilityGenerator : MonoBehaviour
{
  [Header("Rocking Settings")]
  public float maxRockingOffset = 3f;
  public float rockingIntervalMin = 0.5f;
  public float rockingIntervalMax = 2f;

  [Header("Shaking Settings")]
  public float shakingIntervalMin = 1f;
  public float shakingIntervalMax = 8f;
  public float shakingDurationMin = 0.25f;
  public float shakingDurationMax = 2f;
  public float shakingAmplitude = 0.1f;

  private Vector2 m_rockingTarget;
  private float m_rockingInterval;
  private float m_rockingTimer;
  private Vector2 m_rockingVelocity;
  private Vector2 m_starting_position;
  private Vector2 m_canonical_position;
  private bool m_isShaking;
  private float m_shakingTimer;

  void Start()
  {
    m_starting_position = m_canonical_position = transform.position;

    m_rockingTimer = m_rockingInterval = getRockingInterval();
    m_rockingTarget = getRockingTarget();

    m_isShaking = false;
    m_shakingTimer = getShakingInterval();
  }

  // Update is called once per frame
  void Update()
  {
    if (!GameManager.instance.isPlaying)
    {
      return;
    }
    
    m_canonical_position = Vector2.SmoothDamp(m_canonical_position, m_rockingTarget, ref m_rockingVelocity, m_rockingInterval);
    transform.position = m_canonical_position + getShakingOffset();

    m_rockingTimer -= Time.deltaTime;
    m_shakingTimer -= Time.deltaTime;

    if (m_rockingTimer <= 0)
    {
      m_rockingTimer = m_rockingInterval = getRockingInterval();
      m_rockingTarget = getRockingTarget();
    }

    if (m_shakingTimer <= 0)
    {
      m_shakingTimer = m_isShaking ? getShakingInterval() : getShakingDuration();
      m_isShaking = !m_isShaking;
    }
  }

  Vector2 getRockingTarget()
  {
    return m_starting_position + new Vector2(Random.Range(-maxRockingOffset, maxRockingOffset), Random.Range(-maxRockingOffset, maxRockingOffset));
  }

  float getRockingInterval()
  {
    return Random.Range(rockingIntervalMin, rockingIntervalMax);
  }

  float getShakingInterval()
  {
    return Random.Range(shakingIntervalMin, shakingIntervalMax);
  }

  float getShakingDuration()
  {
    return Random.Range(shakingDurationMin, shakingDurationMax);
  }

  Vector2 getShakingOffset()
  {
    if (!m_isShaking) return Vector2.zero;
    return new Vector2(Random.Range(-shakingAmplitude, shakingAmplitude), Random.Range(-shakingAmplitude, shakingAmplitude));
  }
}
