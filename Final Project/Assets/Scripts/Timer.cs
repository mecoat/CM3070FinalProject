﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    private float remainingTime = 10;

    private Text timerDisplay;

    private GameManager manager;

    private bool timerRun = true;

    private bool endGameTriggered = false;
    private bool timeWarning1Triggered = false;
    private bool timeWarning2Triggered = false;


    // Start is called before the first frame update
    void Start()
    {
        timerDisplay = GameObject.Find("Counter").GetComponent<Text>();

        manager = GameObject.Find("Manager").GetComponent<GameManager>();

        //remainingTime = (float)manager.getTimer();
        //Debug.Log(remainingTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0 && timerRun)
        {
            remainingTime -= Time.deltaTime;

            //Debug.Log(remainingTime);

            int intMins = (int)(remainingTime / 60);
            string mins = intMins.ToString();

            if (mins.Length < 2)
            {
                mins = "0" + mins;
            }

            int intSecs = (int)Mathf.Ceil(remainingTime % 60);
            string secs = intSecs.ToString();

            if (secs.Length < 2)
            {
                secs = "0" + secs;
            }

            string disp = "Time Remaining" + "\n" + mins + ":" + secs;

            //Debug.Log(disp);

            timerDisplay.text = disp;
        }
        else if (remainingTime <= 0 && !endGameTriggered)
        {
            endGameTriggered = true;
            manager.endGame(true, false);
        }

        if (remainingTime < 5 && !timeWarning2Triggered)
        {
            Debug.Log("Change to red");
            //RenderSettings.skybox.SetColor("_Tint", Color.red);
            Camera.main.backgroundColor = new Color32(152, 11, 42, 255);
            timeWarning2Triggered = true;
        } else if (remainingTime < 10 && !timeWarning1Triggered)
        {
            Debug.Log("Change to orange");
            Camera.main.backgroundColor = new Color32(163, 90, 0, 255);
            timeWarning1Triggered = true;
        }

    }

    public void setTimer(int timeVal)
    {
        remainingTime = (float)timeVal;
    }

    public void stopTimer()
    {
        timerRun = false;
    }

    public int getTimer()
    {
        return ((int)Mathf.Ceil(remainingTime));
    }
}
