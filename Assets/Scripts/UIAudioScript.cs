using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class UIAudioScript : MonoBehaviour
{

    [SerializeField] private AudioSource audioClick;
    [SerializeField] private AudioSource playerHurt;
    [SerializeField] private AudioSource audioBackgroundMusic;
    [SerializeField] private AudioSource audioDrums;

    public void PlayUIClick()
    {
        audioClick.Play();
    }
    public void PlayPlayerHurt()
    {
        playerHurt.Play();
    }
    public void PlayBackgroundMusic()
    {
        if (audioBackgroundMusic != null) audioBackgroundMusic.Play();
        if (audioDrums != null) audioDrums.Play();
    }
    public void PauseBackgroundMusic()
    {
        if (audioBackgroundMusic != null) audioBackgroundMusic.Pause();
        if (audioDrums != null) audioDrums.Pause();
    }
}
