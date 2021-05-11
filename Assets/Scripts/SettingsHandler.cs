using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;
public static class SettingsHandler
{
    public static void UpdateVolumeUI(SettingsObject loadedSettings, Slider slider)
    {
        slider.value = loadedSettings.volume;
    }
    public static void UpdateFullscreenUI(SettingsObject loadedSettings, Toggle toggle)
    {
        toggle.isOn = loadedSettings.fullscreen;
    }
    public static void UpdateDropdownUI(SettingsObject loadedSettings, Resolution[] resolutions, TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();
        List<string> resolutionStrings = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
            resolutionStrings.Add(resolutions[i].ToString());
        dropdown.AddOptions(resolutionStrings);
        dropdown.value = loadedSettings.selectedResolution;
        dropdown.RefreshShownValue();
    }

    public static void ChangeFullscreen(SettingsObject loadedSettings)
    {
        Screen.fullScreen = loadedSettings.fullscreen;
    }

    public static void ChangeVolume(SettingsObject loadedSettings, AudioMixer audioMixer)
    {
        audioMixer.SetFloat("volume", loadedSettings.volume);
    }

    public static void ChangeResolution(SettingsObject loadedSettings, Resolution[] resolutions)
    {
        Resolution res = resolutions[loadedSettings.selectedResolution];
        Screen.SetResolution(res.width, res.height, loadedSettings.fullscreen);
    }

    public static SettingsObject LoadSettings()
    {

        SettingsObject loadedSettings;
        try
        {
            string file = File.ReadAllText(Application.persistentDataPath + "/settings.json");
            loadedSettings = JsonUtility.FromJson<SettingsObject>(file);

        }
        catch (FileNotFoundException)
        {

            var fs = new FileStream(Application.persistentDataPath + "/settings.json", FileMode.Create);
            fs.Dispose();
            loadedSettings = new SettingsObject(true, 0, 1.0f);
            SaveSettings(loadedSettings);
        }
        return loadedSettings;
    }
    public static void SaveSettings(SettingsObject loadedSettings)
    {
        string jsonFormat = JsonUtility.ToJson(loadedSettings);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", jsonFormat);

    }
}
