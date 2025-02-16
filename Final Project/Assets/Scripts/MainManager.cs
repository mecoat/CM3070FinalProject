using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//from https://learn.unity.com/tutorial/implement-data-persistence-between-scenes# accessed 18/1/24
public class MainManager : MonoBehaviour
{

    private int currLevel = 1;

    private int playerScore = 0;

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
        playerScore = playerScore + newVal;

        Debug.Log("player score = " + playerScore);
    }
}
