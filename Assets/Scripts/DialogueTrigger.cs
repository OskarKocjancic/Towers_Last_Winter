﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool triggerOnLoad;
    public Dialogue dialogue;


    private void Update()
    {
        SkipDialogue();
    }
    private void Awake()
    {
        if (triggerOnLoad)
        {
            TriggerDialogue();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void SkipDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<DialogueManager>().nextLine();
        }
    }
}
