using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI enterBox;
    [SerializeField] private bool isLastScene;
    public Queue<string> lines = new Queue<string>();


    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogueBox != null && enterBox != null)
        {
            Debug.Log("Hello World");

            dialogueBox.enabled = true;
            enterBox.enabled = true;
        }
        FindObjectOfType<PlayerContoller>().enabled = false;
        lines.Clear();

        foreach (var line in dialogue.lines)
        {
            lines.Enqueue(line);
        }
        nextLine();

    }
    public void nextLine()
    {
        if (lines.Count != 0)
        {
            dialogueBox.text = lines.Dequeue();
        }
        else
        {

            EndDialogue();
        }
    }
    public void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        FindObjectOfType<DialogueTrigger>().enabled = false;
        FindObjectOfType<PlayerContoller>().enabled = true;
        if (isLastScene)
        {
            FindObjectOfType<SceneTransition>().FadeToBlack();
            SceneManager.LoadScene("EndScene");
        }
    }
}
