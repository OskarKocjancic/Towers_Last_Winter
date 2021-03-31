using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    private SettingsObject loadedSettings;

    private void Awake()
    {
        loadedSettings = SettingsHandler.LoadSettings();
        SettingsHandler.UpdateVolumeUI(loadedSettings, slider);
    }

    public void SetVolume(float vol)
    {
        loadedSettings.volume = vol;
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
