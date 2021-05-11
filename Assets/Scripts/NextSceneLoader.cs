using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class NextSceneLoader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("LoadNextScene", 0.3f);
    }
    public void LoadNextScene()
    {
        FindObjectOfType<SceneTransition>().FadeToBlack();

        int sceneCounter = PlayerPrefs.GetInt("Level");
        int currentScene = int.Parse(Regex.Split(SceneManager.GetActiveScene().name, "Level")[1]);

        if (sceneCounter <= currentScene)
        {
            PlayerPrefs.SetInt("Level", currentScene);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
