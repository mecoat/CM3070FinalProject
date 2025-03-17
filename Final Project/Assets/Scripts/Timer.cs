using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    //variable to hold the remaining time for this level
    private float remainingTime = 10;


    //holder for the timer display
    [SerializeField]
    Text timerDisplay;

    //holder for the game manager
    [SerializeField]
    GameManager manager;

    //boolean to determine if the timer should be running (counting down)
    private bool timerRun = true;

    //boolean to determine if the endGame function has been triggered
    private bool endGameTriggered = false;
    //boolean to determine if the first time warning has been trigered
    private bool timeWarning1Triggered = false;
    //boolean to determine if the second time warning has been triggered
    private bool timeWarning2Triggered = false;

    // Update is called once per frame
    void Update()
    {
        //check if the reamining time is greater than 0 (hasn't run out yet), and if the timer should still be running
        if (remainingTime > 0 && timerRun)
        {
            //subtract Time.deltaTime from the ramaining time (this removes the time for each frame each time this function is called)
            remainingTime -= Time.deltaTime;

            //calculate the number of minutes rmemaining in the time
            int intMins = (int)(remainingTime / 60);
            //conert the integer to a string
            string mins = intMins.ToString();

            //if there aren't 2 digits in the minutes length...
            if (mins.Length < 2)
            {
                //add a "0" on the front
                mins = "0" + mins;
            }

            //calulate the number of seconds (excluding minutes) remaining
            int intSecs = (int)Mathf.Ceil(remainingTime % 60);
            //convert the above into a string
            string secs = intSecs.ToString();

            //if the seconds string is not 2 digits long...
            if (secs.Length < 2)
            {
                //add a 0 on the front
                secs = "0" + secs;
            }

            //create the display string
            string disp = "Time Remaining" + "\n" + mins + ":" + secs;

            //change the display tect to current time
            timerDisplay.text = disp;
        }
        //otherwise, if remaining time has run out (<= 0) and endgame hasn't already been triggered
        else if (remainingTime <= 0 && !endGameTriggered)
        {
            //change the value of endGameTriggered so this doesn't keep getting triggered
            endGameTriggered = true;
            //call endGame on gameManager
            manager.endGame(true, false);
        }

        //trigger the warning changes
        //if there is less than 5 seconds left and second warning has not been triggered
        if (remainingTime < 5 && !timeWarning2Triggered)
        {
            //change the colour of the background to red            
            Camera.main.backgroundColor = new Color32(152, 11, 42, 255);
            //change the value of the timeWarning2Triggered variable so this doesn't keep getting triggered
            timeWarning2Triggered = true;
        } 
        //otherwise if there is less than 10 seconds and the first warning hasn't been triggered..
        else if (remainingTime < 10 && !timeWarning1Triggered)
        {
            //change the colour of the background to orange
            Camera.main.backgroundColor = new Color32(163, 90, 0, 255);
            //change the value of the timeWarning1Triggered variable so this doesn't keep getting triggered
            timeWarning1Triggered = true;
        }
    }

    //set the timer value
    public void setTimer(int timeVal)
    {
        //setthe timer value to a float of the incoming integer
        remainingTime = (float)timeVal;
    }

    //stop the timer
    public void stopTimer()
    {
        //change timerRun too false so the timer stop counting down
        timerRun = false;
    }

    //get the timer value
    public int getTimer()
    {
        //return the value of the reamining time 
        return ((int)Mathf.Ceil(remainingTime));
    }
}
