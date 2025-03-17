using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenie : MonoBehaviour
{
    //Saves a screenshot when a button is pressed

    //hold a secific keyCode
    public KeyCode screenShotButton;

    // Update is called once per frame
    void Update()
    {
        //if the specifickey is pressed...
        if (Input.GetKeyDown(screenShotButton))
        {
            //take a screenshot and save to the specified location
            ScreenCapture.CaptureScreenshot("Assets/Scripts/Resources/TargetImages/Yellow Sphere.png");
            //debug log to indicate the successful screenshot
            Debug.Log("A screenshot was taken!");
        }
    }
}
