using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class SettingsButtons : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private AudioMixer audioMixer;
    public Resolution[] resolutions;
    public SettingsObject loadedSettings;
    private void Start()
    {
        resolutions = Screen.resolutions;
        LoadSettings();
        UpdateDropdown();
    }
    public void ChangeVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
        if (loadedSettings != null)
        {
            loadedSettings.volume = vol;
            SaveSettings(loadedSettings.fullscreen, loadedSettings.selectedResolution, vol);
            FindObjectOfType<Slider>().value = vol;
        }

    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        loadedSettings.fullscreen = fullscreen;
        SaveSettings(fullscreen, loadedSettings.selectedResolution, loadedSettings.volume);
        FindObjectOfType<Toggle>().isOn = fullscreen;
    }

    public void UpdateDropdown()
    {
        dropdown.ClearOptions();
        List<string> resolutionStrings = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
            resolutionStrings.Add(resolutions[i].ToString());
        dropdown.AddOptions(resolutionStrings);
        dropdown.value = loadedSettings.selectedResolution;
        dropdown.RefreshShownValue();

    }
    public void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        loadedSettings.selectedResolution = index;
        SaveSettings(loadedSettings.fullscreen, index, loadedSettings.volume);
        Screen.SetResolution(res.width, res.height, loadedSettings.fullscreen);
    }
    public void LoadSettings()
    {
        try
        {

            string file = File.ReadAllText(Application.persistentDataPath + "/settings.json");

            loadedSettings = JsonUtility.FromJson<SettingsObject>(file);

        }
        catch (FileNotFoundException)
        {
            var fs = new FileStream(Application.persistentDataPath + "/settings.json", FileMode.Create);
            fs.Dispose();
            loadedSettings = new SettingsObject(false, 0, 1.0f);
            SaveSettings(loadedSettings.fullscreen, loadedSettings.selectedResolution, loadedSettings.volume);
        }
        finally
        {
            ChangeVolume(loadedSettings.volume);
            ChangeResolution(loadedSettings.selectedResolution);
            SetFullscreen(loadedSettings.fullscreen);
        }

    }
    public void SaveSettings(bool fullscreen, int resolution, float volume)
    {
        loadedSettings = new SettingsObject(fullscreen, resolution, volume);
        string jsonFormat = JsonUtility.ToJson(loadedSettings);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", jsonFormat);

    }
}
