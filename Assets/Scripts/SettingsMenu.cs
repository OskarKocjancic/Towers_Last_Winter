using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Slider slider;
    private SettingsObject loadedSettings;
    private Resolution[] resolutions;
    private UIAudioScript audioScript;

    private void Awake()
    {
        audioScript = FindObjectOfType<UIAudioScript>();
        audioScript.PauseBackgroundMusic();
        resolutions = Screen.resolutions;
        loadedSettings = SettingsHandler.LoadSettings();
        SettingsHandler.UpdateFullscreenUI(loadedSettings, toggle);
        SettingsHandler.UpdateVolumeUI(loadedSettings, slider);
        SettingsHandler.UpdateDropdownUI(loadedSettings, resolutions, dropdown);
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
        SettingsHandler.ChangeResolution(loadedSettings, resolutions);
        SettingsHandler.ChangeFullscreen(loadedSettings);
        audioScript.PlayBackgroundMusic();

    }

    public void ChangeVolume(float vol)
    {
        loadedSettings.volume = vol;
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
        SettingsHandler.SaveSettings(loadedSettings);
    }

    public void SetFullscreen(bool fullscreen)
    {
        loadedSettings.fullscreen = fullscreen;
        SettingsHandler.ChangeFullscreen(loadedSettings);
        SettingsHandler.SaveSettings(loadedSettings);

    }
    public void SetResolution(int index)
    {
        loadedSettings.selectedResolution = index;
        SettingsHandler.ChangeResolution(loadedSettings, resolutions);
        SettingsHandler.SaveSettings(loadedSettings);

    }
}
