using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartKeyListener : MonoBehaviour
{
    //the Start game button in the scene
    [SerializeField]
    Button StartGameButton;

    // Update is called once per frame
    void Update()
    {
        //If the fires button is pressed (left alt)
        if (Input.GetButtonDown("Fire2"))
        {
            //invoke the StartGameButton function as though it was clicked
            StartGameButton.onClick.Invoke(); 
        }

    }
}
