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

    private void Start()
    {
        loadedSettings = SettingsHandler.LoadSettings();
        SettingsHandler.UpdateVolumeUI(loadedSettings, slider);
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
    }
    public void SetVolume(float vol)
    {
        loadedSettings.volume = vol;
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
        SettingsHandler.SaveSettings(loadedSettings);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
