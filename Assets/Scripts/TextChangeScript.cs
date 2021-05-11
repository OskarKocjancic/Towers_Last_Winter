using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChangeScript : MonoBehaviour
{
    private GameObject m_beatController;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        m_beatController = GameObject.FindGameObjectWithTag("beatSpawner");
        m_beatController.GetComponent<PlayBeatOnTime>().changeState += BeatComponentScript_changeState;
        m_beatController.GetComponent<PlayBeatOnTime>().resetState += BeatComponentScript_resetState;
    }

    private void BeatComponentScript_resetState(object sender, PlayBeatOnTime.AllCounters e)
    {
        switch (gameObject.name.ToUpper())
        {
            case "BLUE":
                text.SetText(gameObject.name.ToUpper() + ": " + e.blueCounter);
                break;
            case "RED":
                text.SetText(gameObject.name.ToUpper() + ": " + e.redCounter);
                break;
            case "GREEN":
                text.SetText(gameObject.name.ToUpper() + ": " + e.greenCounter);
                break;
            default:
                break;
        }


    }

    private void BeatComponentScript_changeState(object sender, PlayBeatOnTime.OnInput e)
    {
        if (gameObject.name.ToUpper().Equals(e.beat.color.ToString().ToUpper()))
            text.SetText(gameObject.name.ToUpper() + ": " + e.beatCounter);
    }
}



