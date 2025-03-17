using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreInput : MonoBehaviour
{
    //list of possible characters for player input
    private List<string> inputChars = new List<string>(){"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                                        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                        " ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

    //holders for the first input on screen and the currently selected value
    [SerializeField]
    List<GameObject> input0;
    private int input0Loc = 0;

    //holders for the second input on screen and the currently selected value
    [SerializeField]
    List<GameObject> input1;
    private int input1Loc = 0;

    //holders for the third input on screen and the currently selected value
    [SerializeField]
    List<GameObject> input2;
    private int input2Loc = 0;

    //holder for the currently active input
    private int activeInput = 0;

    // Update is called once per frame
    void Update()
    {
        //if the input) object is active in the hierarchy of objects (it, not deactivated, so the player input is active (these are all or nothing and are not done individually)
        if (input0[0].activeInHierarchy)
        {
            //if the up arrow is pressed...
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //run updateInputLoc with a value of -1 (move backwards through the charachers)
                updateInputLoc(-1);
            } 
            //otherwise, it the input is the down arrow...
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //run updateInputLoc with a value of 1 (move forwards through the charachers)
                updateInputLoc(1);
            } 
            //otherwise if the input is the ;eft arrow...
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //subtract 1 from the activeInput (move active input left)
                activeInput -= 1;
                //check that the value hasn't gone out of range
                checkActiveInput();
            }
            //otherwise, if the input is the right arrow...
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //add 1 to Active inut (move active input right)
                activeInput += 1;
                //check that the value hasn't gone out of range
                checkActiveInput();
            }
        }
    }

    //fuction to check if the active input is out of range
    private void checkActiveInput()
    {
        //if the input is less than 0 (too far left), make activeInput 2 (cycle to the right end)
        if (activeInput < 0) { activeInput = 2; };
        //if the activeinput is greater than 2 (too far right), make activeInput 0 (cycle to the left end
        if (activeInput > 2) { activeInput = 0; };

        //run function to update the backgroud on the inputs to indicate the active input
        updateActiveBackground();
    }

    //function to update the input location by the value passed in
    private void updateInputLoc(int valToChange)
    {
        //if the active input is 0 (work with the input0Loc values)
        if (activeInput == 0)
        {
            //add the val tochange to the input loc (currently selected character)
            input0Loc += valToChange;

            //if the value is less than 0, make the value equivalent to the last value in the charachter list (to cycle the charachters)
            if (input0Loc < 0) { input0Loc = inputChars.Count - 1; };
            //if the value is greaterthan or equal to the count of objects in the list, make the value 0 (to cycle the charachters)
            if (input0Loc >= inputChars.Count) { input0Loc = 0; };
        }
        else if (activeInput == 1)
        {
            //add the val tochange to the input loc (currently selected character)
            input1Loc += valToChange;

            //if the value is less than 0, make the value equivalent to the last value in the charachter list (to cycle the charachters)
            if (input1Loc < 0) { input1Loc = inputChars.Count - 1; };
            //if the value is greaterthan or equal to the count of objects in the list, make the value 0 (to cycle the charachters)
            if (input1Loc >= inputChars.Count) { input1Loc = 0; };
        }
        else if (activeInput == 2)
        {
            //add the val tochange to the input loc (currently selected character)
            input2Loc += valToChange;

            //if the value is less than 0, make the value equivalent to the last value in the charachter list (to cycle the charachters)
            if (input2Loc < 0) { input2Loc = inputChars.Count - 1; };
            //if the value is greaterthan or equal to the count of objects in the list, make the value 0 (to cycle the charachters)
            if (input2Loc >= inputChars.Count) { input2Loc = 0; };
        }

        //run function to update the active display
        updateActiveDisp();
    }

    //function to update the active display
    private void updateActiveDisp()
    {
        //if the active input is 0...
        if (activeInput == 0)
        {
            //if the input loc is 0...
            if (input0Loc == 0)
            {
                //then the top character in the input is the last character in the list (to loop the list)
                input0[1].GetComponent<Text>().text = inputChars[inputChars.Count -1];
            } 
            //otherwise...
            else
            {
                //the top character in the input is the character immediately before the input loc in the list
                input0[1].GetComponent<Text>().text = inputChars[input0Loc - 1];
            }

            //the middle character in the input (main) is the input loc value in the list
            input0[2].GetComponent<Text>().text = inputChars[input0Loc];

            //if the input loc is the last character in the character list...
            if (input0Loc == inputChars.Count -1)
            {
                //then the bottom character is the first in the list (to loop the list
                input0[3].GetComponent<Text>().text = inputChars[0];
            }
            //otherwise...
            else
            {
                //the bottom character is the next element in the list from the input loc
                input0[3].GetComponent<Text>().text = inputChars[input0Loc + 1];
            }

        }
        //otherwise, if the active input is 1...
        else if (activeInput == 1)
        {
            //if the input loc is 0...
            if (input1Loc == 0)
            {
                //then the top character in the input is the last character in the list (to loop the list)
                input1[1].GetComponent<Text>().text = inputChars[inputChars.Count - 1];
            }
            //otherwise...
            else
            {
                //the top character in the input is the character immediately before the input loc in the list
                input1[1].GetComponent<Text>().text = inputChars[input1Loc - 1];
            }

            //the middle character in the input (main) is the input loc value in the list
            input1[2].GetComponent<Text>().text = inputChars[input1Loc];

            //if the input loc is the last character in the character list
            if (input1Loc == inputChars.Count - 1)
            {
                //then the bottom character is the first in the list (to loop the list...
                input1[3].GetComponent<Text>().text = inputChars[0];
            }
            //otherwise...
            else
            {
                //the bottom character is the next element in the list from the input loc
                input1[3].GetComponent<Text>().text = inputChars[input1Loc + 1];
            }


        }
        //otherwise, if the active input is 2...
        else if (activeInput == 2)
        {
            //if the input loc is 0...
            if (input2Loc == 0)
            {
                //then the top character in the input is the last character in the list (to loop the list)
                input2[1].GetComponent<Text>().text = inputChars[inputChars.Count - 1];
            }
            //otherwise...
            else
            {
                //the top character in the input is the character immediately before the input loc in the list
                input2[1].GetComponent<Text>().text = inputChars[input2Loc - 1];
            }

            //the middle character in the input (main) is the input loc value in the list
            input2[2].GetComponent<Text>().text = inputChars[input2Loc];

            //if the input loc is the last character in the character list...
            if (input2Loc == inputChars.Count - 1)
            {
                //then the bottom character is the first in the list (to loop the list
                input2[3].GetComponent<Text>().text = inputChars[0];
            }
            //otherwise...
            else
            {
                //the bottom character is the next element in the list from the input loc
                input2[3].GetComponent<Text>().text = inputChars[input2Loc + 1];
            }
        }
    }

    //function to update the background colours of the inputs to indicate to the user the active inpput
    private void updateActiveBackground()
    {
        //set the activeColour background values
        Color32 activeColour = new Color32(108, 207, 246, 255);

        //set the inactiveColour background values
        Color32 inactiveColour = new Color32(239, 91, 91, 255);

        //if the active input is 0...
        if (activeInput == 0)
        {
            //make the background colour the active colour for this input
            input0[0].GetComponent<Image>().color = activeColour;
        }
        //otherwise...
        else
        {
            //make the background colour the inactive Colour for this input
            input0[0].GetComponent<Image>().color = inactiveColour;
        }

        //if the active input is 1...
        if (activeInput == 1)
        {
            //make the background colour the active colour for this input
            input1[0].GetComponent<Image>().color = activeColour;
        }
        //otherwise...
        else
        {
            //make the background colour the inactive Colour for this input
            input1[0].GetComponent<Image>().color = inactiveColour;
        }

        //if the active input is 2...
        if (activeInput == 2)
        {
            //make the background colour the active colour for this input
            input2[0].GetComponent<Image>().color = activeColour;
        }
        //otherwise...
        else
        {
            //make the background colour the inactive Colour for this input
            input2[0].GetComponent<Image>().color = inactiveColour;
        }
    }

    //function to return the user input values
    public string getUserInits()
    {
        //return a string madeup from the values from each input location within the Character list.
        return (inputChars[input0Loc] + inputChars[input1Loc] + inputChars[input2Loc]);
    }
}
