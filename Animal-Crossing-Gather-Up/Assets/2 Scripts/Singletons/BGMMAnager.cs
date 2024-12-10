using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMAnager : SingletonManager<BGMMAnager>
{
    [SerializeField] private AudioSource titleBGM;
    [SerializeField] private AudioSource mainBGM;

    [SerializeField] private AudioClip titleBGMClip;
    [SerializeField] private AudioClip mainBGMClip;

    void Start()
    {
        PlayTitleMusic();
    }
    public void StopTitleMusic()
    {
        titleBGM.Stop();
    }

    public void StopMainMusic()
    {
        mainBGM.Stop();
    }

    public void PlayTitleMusic()
    {
        titleBGM.Play();
    }

    public void PlayMainMusic()
    {
        mainBGM.Play();
    }
}


