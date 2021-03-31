using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("loadNextScene", 0.3f);
    }
    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
