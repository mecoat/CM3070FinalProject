using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenie : MonoBehaviour
{
    //Saves a screenshot when a button is pressed

    public KeyCode screenShotButton;

    void Update()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            ScreenCapture.CaptureScreenshot("Assets/Scripts/Resources/TargetImages/Yellow Sphere.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
