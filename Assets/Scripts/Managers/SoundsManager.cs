using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    public Sound[] sounds;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;

            s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.volume;
            s.audioSource.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogError("sound: " + s.name + "was not found");
            return;
        }


        s.audioSource.Play();
    }

}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    public bool playOnAwake = true;

    [Range(0, 1)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSource;
}