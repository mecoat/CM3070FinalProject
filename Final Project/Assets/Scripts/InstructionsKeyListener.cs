using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstructionsKeyListener : MonoBehaviour
{
    //the Start Game button
    [SerializeField]
    Button StartGameButton;

    //the retunr to start screen button
    [SerializeField]
    Button MenuGameButton;

    // Update is called once per frame
    void Update()
    {
        //if the fire2 (left alt) button is pressed
        if (Input.GetButtonDown("Fire2"))
        {
            //run the function for when the Start Game button is clicked
            StartGameButton.onClick.Invoke();
        }

        //if the Fire1 (Left Ctrl) is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            //run the function for when the Menu button is pressed
            MenuGameButton.onClick.Invoke();
        }
    }
}
