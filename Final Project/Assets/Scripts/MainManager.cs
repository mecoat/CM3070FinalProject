using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//from https://learn.unity.com/tutorial/implement-data-persistence-between-scenes# accessed 18/1/24
public class MainManager : MonoBehaviour
{

    private int currLevel = 1;

    private int playerScore = 0;

    private List<(string initials, int score)> highScoreBoard = new List<(string, int)>();

    public static MainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int getLevel()
    {
        return currLevel;
    }

    public void resetLevel()
    {
        currLevel = 1;
    }

    public void moveLevel()
    {
        currLevel += 1;
    }

    public int getScore()
    {
        return playerScore;
    }

    public void resetScore()
    {
        playerScore = 0;
    }

    public void updateScore(int newVal)
    {
        playerScore = newVal;
        //playerScore = playerScore + newVal;

        //Debug.Log("player score = " + playerScore);
    }

    public void addToHighScore(string submitInitials, int submitScore)
    {
        if (highScoreBoard.Count == 0)
        {
            highScoreBoard.Add((submitInitials, submitScore));
        } 
        else
        {
            for (var i = highScoreBoard.Count-1; i >= 0; i--)
            {
                if (submitScore == highScoreBoard[i].score || submitScore < highScoreBoard[i].score)
                {
                    Debug.Log("need to add score)");
                    if (i == highScoreBoard.Count - 1)
                    {
                        //Debug.Log("adding");
                        highScoreBoard.Add((submitInitials, submitScore));
                    } 
                    else
                    {
                        //Debug.Log("inserting");
                        highScoreBoard.Insert(i + 1, (submitInitials, submitScore));
                    }

                    if (highScoreBoard.Count > 10)
                    {
                        highScoreBoard.RemoveAt(highScoreBoard.Count - 1);
                    }

                    break;
                }
                else if (i == 0 && submitScore > highScoreBoard[i].score)
                {
                    highScoreBoard.Insert(i, (submitInitials, submitScore));
                    break;
                }
            }

        }

        for (var i = 0; i < highScoreBoard.Count; i++)
        {
            Debug.Log(highScoreBoard[i]);
        }
    }
}
