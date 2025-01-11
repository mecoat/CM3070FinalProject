using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    private float remainingTime = 10;

    private Text timerDisplay;

    private GameManager manager;


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
        if (remainingTime > 0)
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
        else
        {
            manager.endGame(true, false);
        }

    }

    public void setTimer(int timeVal)
    {
        remainingTime = (float)timeVal;
    }
}
