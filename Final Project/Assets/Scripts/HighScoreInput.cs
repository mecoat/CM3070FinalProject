using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreInput : MonoBehaviour
{
    private List<string> inputChars = new List<string>(){"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                                        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                        " ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

    [SerializeField]
    List <GameObject> input0;
    private int input0Loc = 0;

    [SerializeField]
    List<GameObject> input1;
    private int input1Loc = 0;

    [SerializeField]
    List<GameObject> input2;
    private int input2Loc = 0;

    private int activeInput = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (input0[0].activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                input0Loc -= 1;
                checkInputLoc();
                Debug.Log(input0Loc);
            } 
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                input0Loc += 1;
                checkInputLoc();
                Debug.Log(input0Loc);
            } 
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                activeInput -= 1;
                checkActiveInput();
                Debug.Log(activeInput);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                activeInput += 1;
                checkActiveInput();
                Debug.Log(activeInput);
            }

        }
        
    }

    private void checkActiveInput()
    {
        if (activeInput < 0) { activeInput = 2; };
        if (activeInput > 2) { activeInput = 0; };
    }
    
    private void checkInputLoc()
    {

        if (activeInput == 0)
        {
            if (input0Loc < 0) { input0Loc = inputChars.Count - 1; };
            if (input0Loc >= inputChars.Count) { input0Loc = 0; };
        }
        else if (activeInput == 1)
        {
            if (input1Loc < 0) { input1Loc = inputChars.Count - 1; };
            if (input1Loc >= inputChars.Count) { input1Loc = 0; };
        }
        else if (activeInput == 2)
        {
            if (input2Loc < 0) { input2Loc = inputChars.Count - 1; };
            if (input2Loc >= inputChars.Count) { input2Loc = 0; };
        }
    }
}
