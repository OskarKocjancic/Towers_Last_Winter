using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfObjectsToPause = new List<GameObject>();
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;

    private SettingsObject loadedSettings;
    private Resolution[] resolutions;
    private UIAudioScript audioScript;


    private void Awake()
    {
        audioScript = FindObjectOfType<UIAudioScript>();
        audioScript.PauseBackgroundMusic();
        resolutions = Screen.resolutions;
        loadedSettings = SettingsHandler.LoadSettings();
        SettingsHandler.ChangeVolume(loadedSettings, audioMixer);
        SettingsHandler.ChangeResolution(loadedSettings, resolutions);
        SettingsHandler.ChangeFullscreen(loadedSettings);
        audioScript.PlayBackgroundMusic();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
            PauseGame();
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1.0f)
        {
            foreach (GameObject gameObject in listOfObjectsToPause)
            {
                MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }
            }

            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            audioScript.PauseBackgroundMusic();

        }
        else
        {
            foreach (GameObject gameObject in listOfObjectsToPause)
            {
                MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }
            }
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            audioScript.PlayBackgroundMusic();

        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlaySound()
    {
        audioSource.Play();
    }
}
