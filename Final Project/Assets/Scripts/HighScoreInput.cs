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

        updateActiveBackground();

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

        updateActiveDisp();
    }

    private void updateActiveDisp()
    {
        if (activeInput == 0)
        {
            if (input0Loc == 0)
            {
                input0[1].GetComponent<Text>().text = inputChars[inputChars.Count -1];
            } 
            else
            {
                input0[1].GetComponent<Text>().text = inputChars[input0Loc - 1];
            }

            input0[2].GetComponent<Text>().text = inputChars[input0Loc];

            if (input0Loc == inputChars.Count -1)
            {
                input0[3].GetComponent<Text>().text = inputChars[0];
            }
            else
            {
                input0[3].GetComponent<Text>().text = inputChars[input0Loc + 1];
            }

        }
        else if (activeInput == 1)
        {
            if (input0Loc == 0)
            {
                input1[1].GetComponent<Text>().text = inputChars[inputChars.Count - 1];
            }
            else
            {
                input1[1].GetComponent<Text>().text = inputChars[input1Loc - 1];
            }

            input1[2].GetComponent<Text>().text = inputChars[input1Loc];

            if (input0Loc == inputChars.Count - 1)
            {
                input1[3].GetComponent<Text>().text = inputChars[0];
            }
            else
            {
                input1[3].GetComponent<Text>().text = inputChars[input1Loc + 1];
            }


        }
        else if (activeInput == 2)
        {
            if (input0Loc == 0)
            {
                input2[1].GetComponent<Text>().text = inputChars[inputChars.Count - 1];
            }
            else
            {
                input2[1].GetComponent<Text>().text = inputChars[input2Loc - 1];
            }

            input2[2].GetComponent<Text>().text = inputChars[input2Loc];

            if (input0Loc == inputChars.Count - 1)
            {
                input2[3].GetComponent<Text>().text = inputChars[0];
            }
            else
            {
                input2[3].GetComponent<Text>().text = inputChars[input2Loc + 1];
            }
        }
    }


    private void updateActiveBackground()
    {
        if (activeInput == 0)
        {
            input0[0].GetComponent<Image>().color = new Color(0, 255, 0);
        }
        else
        {
            input0[0].GetComponent<Image>().color = new Color(255, 0, 0);
        }

        if (activeInput == 1)
        {
            input1[0].GetComponent<Image>().color = new Color(0, 255, 0);
        }
        else
        {
            input1[0].GetComponent<Image>().color = new Color(255, 0, 0);
        }

        if (activeInput == 2)
        {
            input2[0].GetComponent<Image>().color = new Color(0, 255, 0);
        }
        else
        {
            input2[0].GetComponent<Image>().color = new Color(255, 0, 0);
        }
    }
}
