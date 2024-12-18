using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioClip[] songs;
    private AudioSource musicSource;
    private int currentSongIndex = -1;

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        PlayNextSong();
    }

    public void PlayNextSong()
    {
        if (songs.Length > 0)
        {
            currentSongIndex = (currentSongIndex + 1) % songs.Length;
            musicSource.clip = songs[currentSongIndex];
            musicSource.Play();
        }
    }

}
