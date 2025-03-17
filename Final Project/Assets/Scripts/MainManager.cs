using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//from https://learn.unity.com/tutorial/implement-data-persistence-between-scenes# accessed 18/1/24
public class MainManager : MonoBehaviour
{
    //holder for the current level value
    private int currLevel = 1;

    //holder for current player's score
    private int playerScore = 0;

    //list to hold the high score table values
    private List<(string initials, int score)> highScoreBoard = new List<(string, int)>();

    //holder for the main manager instance
    public static MainManager Instance;

    //runs on first load
    private void Awake()
    {
        //if there's already an Instance
        if (Instance != null)
        {
            //destroy this version
            Destroy(gameObject);
            //end this function (so as not to reset any values)
            return;
        }

        //set the Instance to this object
        Instance = this;

        //iterate 10 times...
        for (int i=0; i<10; i++)
        {
            //to create 10 values in the High score board
            addToHighScore("MCC", 42);
        }

        //prevent this bject from being destroyed between levels
        DontDestroyOnLoad(gameObject);
    }

    //function to return the current level
    public int getLevel()
    {
        //return the current level
        return currLevel;
    }

    //function to reset the level
    public void resetLevel()
    {
        //reset the level to 1
        currLevel = 1;
    }

    //function t move the level onto the next level
    public void moveLevel()
    {
        //add 1 to the current level
        currLevel += 1;
    }

    //function to return the score
    public int getScore()
    {
        //return the current player score
        return playerScore;
    }

    //function to reset the player score
    public void resetScore()
    {
        //set the score back to 0
        playerScore = 0;
    }

    //function to update the score
    public void updateScore(int newVal)
    {
        //set the player score to the input value
        playerScore = newVal;
    }

    //function to add the initials and score to the high score board
    public void addToHighScore(string submitInitials, int submitScore)
    {
        //if there's nothing in the high score board (it's empty)...
        if (highScoreBoard.Count == 0)
        {
            //add the initials and score to the empty list
            highScoreBoard.Add((submitInitials, submitScore));
        }
        //otherwise...
        else
        {
            //iterate through the list from the end towards the start (so that the first location of a higher or equal score is identified)
            for (var i = highScoreBoard.Count - 1; i >= 0; i--)
            {
                //if the score is equal to or less than the value in the high score board at this location...
                if (submitScore == highScoreBoard[i].score || submitScore < highScoreBoard[i].score)
                {
                    //if i is the last value...
                    if (i == highScoreBoard.Count - 1)
                    {
                        ////add the score and initials to the end of the list
                        highScoreBoard.Add((submitInitials, submitScore));
                    }
                    //otherwise (i is not the last value)
                    else
                    {
                        //insert the score and initials at the location after i (ie i+1)
                        highScoreBoard.Insert(i + 1, (submitInitials, submitScore));
                    }

                    //if the highscore board is now longer than 10...
                    if (highScoreBoard.Count > 10)
                    {
                        //remove the last value (as this is a part of the addition, there should only ever be 1 more, so removing 1 should be suficient)...
                        highScoreBoard.RemoveAt(highScoreBoard.Count - 1);
                    }
                    //stop the iteration loop
                    break;
                }
                //otherwise if i is 0 (first value), and the score is still higher...
                else if (i == 0 && submitScore > highScoreBoard[i].score)
                {
                    //insert the score in the first position (as this is a new Highest Score)
                    highScoreBoard.Insert(i, (submitInitials, submitScore));
                    //stop the iteration loop (although if i = 0 this should be the last time anyway)
                    break;
                }
            }
        }
    }

    //function to return the high scores table
    public List<(string, int)> getHighScores()
    {
        //return the highScoreBoard to the calling location
        return highScoreBoard;
    }

    //function to return the lowest high score (to quickly determine if a new score is a high score)
    public int getLowestHighScore()
    {
        //returns the score from the last value in the Hogh score table (as the table is sorted as things are added)
        return (highScoreBoard[highScoreBoard.Count -1].score);
    }
}
