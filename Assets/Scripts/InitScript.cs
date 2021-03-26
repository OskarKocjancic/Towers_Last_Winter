using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InitScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    void Start()
    {
        string file = File.ReadAllText(Application.dataPath + "/settings.json");
        SettingsObject loadedSettings = JsonUtility.FromJson<SettingsObject>(file);
        changeVolume(loadedSettings.volume);
        SetFullscreen(loadedSettings.fullscreen);
        ChangeResolution(loadedSettings.selectedResolution);
    }

    public void changeVolume(float vol)
    {
        
        audioMixer.SetFloat("volume", vol);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeResolution(int index)
    {
        Resolution res = Screen.resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
