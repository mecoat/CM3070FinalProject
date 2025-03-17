using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.c-sharpcorner.com/article/unity-change-scene-on-button-click-using-c-sharp-scripts-in-unity/
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    //function to load the game
    public void StartGame()
    {
        //loads the gaame scene
        SceneManager.LoadScene("GameScene");
    }

    //function to return to the start scene
    public void GoToStart()
    {
        //goes back to start scene
        SceneManager.LoadScene("StartScene");
    }
}
