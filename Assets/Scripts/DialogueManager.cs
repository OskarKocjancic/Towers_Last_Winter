using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueBox;
    public Queue<string> lines = new Queue<string>();


    public void StartDialogue(Dialogue dialogue) {
        FindObjectOfType<PlayerContoller>().enabled = false;
        lines.Clear();

        foreach ( var line in dialogue.lines)
        {
            lines.Enqueue(line);
        }
        nextLine();

    }
    public void nextLine() {
        if (lines.Count!=0)
        {
            dialogueBox.text = lines.Dequeue();
        }
        else
        {
            EndDialogue();
        }
    }
    public void EndDialogue() {
        dialogueBox.gameObject.SetActive(false);
        FindObjectOfType<DialogueTrigger>().enabled = false;
        FindObjectOfType<PlayerContoller>().enabled = true;
    }
}
