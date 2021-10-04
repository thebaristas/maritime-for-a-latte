using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class Sound
{
  public string name;

  public AudioClip clip;

  public bool loop;
  [Range(0f, 1f)]
  public float volume = 1;
  [Range(0.1f, 3f)]
  public float pitch = 1;

  [HideInInspector]
  public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  public static AudioManager instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
    }
  }

  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s.name == "")
    {
      Debug.LogWarning("Sound not found: " + name);
    }
    if (s.source.time > 0f)
    {
        s.source.UnPause();
    }
    else
    {
        s.source.Play();
    }
  }

  public void Pause(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s.name == "")
    {
      Debug.LogWarning("Sound not found: " + name);
    }
    s.source.Pause();
  }

  public void Stop(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s.name == "")
    {
      Debug.LogWarning("Sound not found: " + name);
    }
    s.source.Stop();
  }
}