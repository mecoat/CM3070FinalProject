using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float remainingTime = 10;

    GameObject timerDisplay;


    // Start is called before the first frame update
    void Start()
    {
        timerDisplay = GameObject.Find("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

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

        string disp = "Time Remaining" + "\n" + mins  + ":" + secs;

        Debug.Log(disp);
    }
}
