using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class UIAudioScript : MonoBehaviour
{

    [SerializeField] private AudioSource audioClick;
    [SerializeField] private AudioSource audioBackgroundMusic;

    public void PlayUIClick()
    {
        audioClick.Play();
    }
    public void PlayBackgroundMusic()
    {
        audioBackgroundMusic.Play();
    }
    public void PauseBackgroundMusic()
    {
        audioBackgroundMusic.Pause();
        
    }
}
