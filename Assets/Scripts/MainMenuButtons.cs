using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private float loadDelay;
    private string nextSceneName;
    public AudioSource audioSource;
    public void loadLevel(string x) {
        nextSceneName = x;
        Invoke("nextScene", loadDelay);
        
    }
    private void nextScene() {
        SceneManager.LoadScene(nextSceneName);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void playSound() {   
        audioSource.Play();
    }
}
