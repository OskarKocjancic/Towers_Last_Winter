using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using TMPro;

public class PlayBeatOnTime : MonoBehaviour
{
    [SerializeField] private ScriptableBeat beatRed;
    [SerializeField] private ScriptableBeat beatBlue;
    [SerializeField] private ScriptableBeat beatGreen;

    [SerializeField] public int redLives;
    [SerializeField] public int blueLives;
    [SerializeField] public int greenLives;

    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceDrums;
    [SerializeField] private float beatDelay;

    [SerializeField] private int redCounter;
    [SerializeField] private int blueCounter;
    [SerializeField] private int greenCounter;
    private float beatDelayCounter;


    //create arguments for event
    public class OnInput : EventArgs
    {
        public ScriptableBeat beat;
        public int beatCounter;
    }
    public class AllCounters : EventArgs
    {
        public int redCounter;
        public int blueCounter;
        public int greenCounter;
    }
    public event EventHandler<OnInput> changeState;
    public event EventHandler<AllCounters> resetState;

    void Awake()
    {
        beatDelayCounter = beatDelay;
        redCounter = redLives;
        blueCounter = blueLives;
        greenCounter = greenLives;
        resetState?.Invoke(this, new AllCounters { redCounter = redLives, blueCounter = blueLives });
    }


    void Update()
    {
        //Debug.Log(new { redCounter, blueCounter, greenCounter });

        if (!audioSourceDrums.isPlaying)
        {
            audioSourceDrums.Play();
            redCounter = redLives;
            blueCounter = blueLives;
            greenCounter = greenLives;
            resetState?.Invoke(this, new AllCounters { redCounter = redLives, blueCounter = blueLives, greenCounter = greenLives });
        }

        if (beatDelayCounter <= 0f)
        {
            if (Input.inputString.ToUpper() == "J" && blueCounter > 0)
            {
                blueCounter--;
                changeState?.Invoke(this, new OnInput { beat = beatBlue, beatCounter = blueCounter });
                beatDelayCounter = beatDelay;
            }
            if (Input.inputString.ToUpper() == "K" && redCounter > 0)
            {
                redCounter--;
                changeState?.Invoke(this, new OnInput { beat = beatRed, beatCounter = redCounter });
                beatDelayCounter = beatDelay;
            }
            if (Input.inputString.ToUpper() == "L" && greenCounter > 0)
            {
                greenCounter--;
                changeState?.Invoke(this, new OnInput { beat = beatGreen, beatCounter = greenCounter });
                beatDelayCounter = beatDelay;
            }

        }
        else
        {
            beatDelayCounter -= Time.deltaTime;
        }


    }
    public void ResetLevel()
    {
        audioSourceMusic.Play();
        redCounter = redLives;
        blueCounter = blueLives;
        greenCounter = greenLives;
        resetState?.Invoke(this, new AllCounters { redCounter = redLives, blueCounter = blueLives, greenCounter = greenLives });
    }
}
