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
    [SerializeField] public int redLives;
    [SerializeField] public int blueLives;
    [SerializeField] private GameObject textRed;
    [SerializeField] private GameObject textBlue;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private float beatDelay;
    private int redCounter;
    private int blueCounter;
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
    }
    public event EventHandler<OnInput> changeState;
    public event EventHandler<AllCounters> resetState;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        beatDelayCounter = beatDelay;
        redCounter = redLives;
        blueCounter = blueLives;
        resetState?.Invoke(this, new AllCounters { redCounter=this.redCounter, blueCounter= this.blueCounter});
    }


    void Update()
    {
        if (!audioSource2.isPlaying)
        {
            
            audioSource2.Play();
            redCounter = redLives;
            blueCounter = blueLives;
            
            resetState?.Invoke(this, new AllCounters { redCounter = redLives, blueCounter = blueLives});
        }

        if (beatDelayCounter<=0f)
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
                changeState?.Invoke(this, new OnInput { beat = beatRed, beatCounter = redCounter});
                beatDelayCounter = beatDelay;
            }
            
        }
        else
        {
            beatDelayCounter -= Time.deltaTime;
        }


    }





}
