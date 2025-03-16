using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scorer : MonoBehaviour
{
    //holder for current score
    private int currScore = 0;

    //the display area in the scene
    [SerializeField]
    Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //run updateScore with a value of 0
        updateScpre(0);
    }

    //function to update the score with the value input as an argument
    public void updateScpre(int addVal)
    {
        //add the input value to the current score variable
        currScore += addVal;
        
        //create a new string to display
        string disp = "Current Score" + "\n" + currScore.ToString();

        //diplaythe string
        scoreDisplay.text = disp;
    }

    //function to feed back the current scrore
    public int getScore()
    {
        //returns the curent score to the requesting location
        return (currScore);
    }
}
