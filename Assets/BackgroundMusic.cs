using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    AudioSource audio;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlayBGMusic(AudioClip clip)
    {
        audio.loop = true;
        audio.clip = clip;
        audio.Play();
    }
    public void PauseBGMusic()
    {
        audio.Pause();
    }
    public void StopBGMusic()
    {
        audio.Stop();
    }
}
