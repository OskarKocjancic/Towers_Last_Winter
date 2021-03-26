using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsHandler : MonoBehaviour
{
    public SettingsObject loadedSettings;
    public SettingsButtons settingsButtons;

    public void loadSettings() {
        string file =File.ReadAllText(Application.dataPath + "/settings.json");
        loadedSettings = JsonUtility.FromJson<SettingsObject>(file);
        settingsButtons.changeVolume(loadedSettings.volume);
        settingsButtons.ChangeResolution(loadedSettings.selectedResolution);
        settingsButtons.SetFullscreen(loadedSettings.fullscreen);
    }
    public void saveSettings(bool fullscreen, int resolution, float volume) {
        loadedSettings = new SettingsObject(fullscreen, resolution, volume);
        string jsonFormat = JsonUtility.ToJson(loadedSettings);
        File.WriteAllText(Application.dataPath + "/settings.json", jsonFormat);
    }
}
