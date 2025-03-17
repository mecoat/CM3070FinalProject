using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScores : MonoBehaviour
{
    //holders for the left and right halves of the high score board display
    [SerializeField]
    GameObject leftDisplay;
    [SerializeField]
    GameObject rightDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //temparoray holder for the high score list, filled from the high scores in Main Manager
        List<(string, int)> highScores = MainManager.Instance.getHighScores();

        //replace the text in the left display with the first half of the high score board
        leftDisplay.GetComponent<Text>().text = createHighScores(0, highScores);
        //replace the text in the right display with the second half of the high score board
        rightDisplay.GetComponent<Text>().text = createHighScores(5, highScores);

    }

    //function to return a string of the high scores in the input list ready for display
    private string createHighScores(int startVal, List<(string, int)> fullListScores)
    {
        //create an empty string
        string newText = "";

        //holder of the length og the list (to prevent referencing out of range
        int listLength = fullListScores.Count;

        //work from the start value for 5 values
        for (int i = startVal; i < (startVal + 5); i++)
        {
            //as long as the value is not out of range...
            if (listLength > i)
            {
                //add the values for this location in the list to the string with a space in between 
                newText += fullListScores[i].Item1 + " " + fullListScores[i].Item2.ToString();
            }
            //otherwise (no value)
            else
            {
                //display a placeholder
                newText += "MCC 42";
            }

            //if this is one of the first 4 values for this function call...
            if (i < startVal + 4)
            {
                //add a new line to the string
                newText += "\n";
            }
        }
        
        //return the string for use
        return newText;
    }
}
