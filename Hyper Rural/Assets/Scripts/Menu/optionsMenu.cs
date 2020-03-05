﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class optionsMenu : MonoBehaviour
{
    private AudioSource audioSrc;

    private float musicVolume = 1f;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
