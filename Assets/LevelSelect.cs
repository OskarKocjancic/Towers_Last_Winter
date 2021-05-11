using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelect : MonoBehaviour
{
    private string nextSceneName;
    [SerializeField] private float loadDelay;

    private void Awake()
    {

        for (int i = 1; i < 9; i++)
        {

            CheckFor(i);
        }
    }
    public void CheckFor(int level)
    {

        int x = PlayerPrefs.GetInt("Level", 1);
        if (x >= level)
        {
            GameObject.Find("Level " + level).GetComponent<Button>().interactable = true;
        }
    }
    public void LoadLevel(string x)
    {
        nextSceneName = x;
        Invoke("NextScene", loadDelay);

    }
    private void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
