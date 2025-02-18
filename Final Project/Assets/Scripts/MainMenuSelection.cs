using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.c-sharpcorner.com/article/unity-change-scene-on-button-click-using-c-sharp-scripts-in-unity/
using UnityEngine.SceneManagement;

public class MainMenuSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Debug.Log("loading scene");

        //loads the gaame
        SceneManager.LoadScene("InstructionScene");
    }
}
