using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsButtons : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private AudioMixer audioMixer;
    public TMP_Text texttest;
    public SettingsHandler settingsHandler;
    public Resolution[] resolutions;

    private void Awake()
    {

    }

    private void Start()
    {
        Debug.Log("Hello World");
        settingsHandler.loadedSettings = new SettingsObject();
        resolutions = Screen.resolutions;
        settingsHandler.loadSettings();
        UpdateDropdown();
    }
    public void changeVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
        settingsHandler.loadedSettings.volume = vol;
        settingsHandler.saveSettings(settingsHandler.loadedSettings.fullscreen, settingsHandler.loadedSettings.selectedResolution, vol);
        FindObjectOfType<Slider>().value= vol;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        settingsHandler.loadedSettings.fullscreen = isFullscreen;
        settingsHandler.saveSettings(isFullscreen, settingsHandler.loadedSettings.selectedResolution, settingsHandler.loadedSettings.volume);
        FindObjectOfType<Toggle>().isOn = isFullscreen;
    }

    public void UpdateDropdown()
    {
        dropdown.ClearOptions();
        List<string> resolutionStrings = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) { 
        
            resolutionStrings.Add(resolutions[i].ToString());
            texttest.text = texttest.text+resolutions[i].ToString();
        }
        
        dropdown.AddOptions(resolutionStrings);
        dropdown.value = settingsHandler.loadedSettings.selectedResolution;
        dropdown.RefreshShownValue();
        
    }
    public void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        settingsHandler.loadedSettings.selectedResolution=index;
        settingsHandler.saveSettings(settingsHandler.loadedSettings.fullscreen, index, settingsHandler.loadedSettings.volume);
        Screen.SetResolution(res.width, res.height, settingsHandler.loadedSettings.fullscreen);
    }
}
