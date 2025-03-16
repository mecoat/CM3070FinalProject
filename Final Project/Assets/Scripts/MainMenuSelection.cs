using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.c-sharpcorner.com/article/unity-change-scene-on-button-click-using-c-sharp-scripts-in-unity/
using UnityEngine.SceneManagement;

public class MainMenuSelection : MonoBehaviour
{
    //function to load the instructions scene
    public void StartGame()
    {
        //loads the instructions page
        SceneManager.LoadScene("InstructionScene");
    }
}
