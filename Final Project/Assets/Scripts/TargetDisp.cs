using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetDisp : MonoBehaviour
{
    //dictionary to hold the list of targets to display and how many are required
    private Dictionary <string,int> targets = new Dictionary<string, int>();

    //the targetholder
    [SerializeField]
    GameObject targetHolder;

    //holder for the target displays
    private List<GameObject> targetDisps = new List<GameObject>();
    
    //list of the keys from the dictionary of targets so they can be iterated through
    private List <string> targetDictKeys;

    // Start is called before the first frame update
    void Start()
    {
        //add the 4 target display locations to the targetDisps list       
        targetDisps.Add(targetHolder.transform.GetChild(2).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(3).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(4).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(5).gameObject);
    }

    //setup the targets dicstionary and keys list
    public void setTargets(Dictionary<string, int> inpTargets)
    {
        //fill the targets dictionary from the input dictionary
        targets = inpTargets;
        
        //fill the keys list with the teys from the target dictionary (the names of the target objects
        targetDictKeys = new List<string>(targets.Keys);

        //run the setup display function
        setupDisplay();
    }


    //function to set up the target display
    private void setupDisplay()
    {
        //iterate through the targets
        for (int i = 0; i < targets.Count; i++)
        {
            //make the display appear
            targetDisps[i].SetActive(true);

            //create a sprite to display
            Sprite targSprite = Sprite.Create(Resources.Load<Texture2D>("TargetImages/" + targetDictKeys[i]),
                                                                                                            new Rect(0f, 0f, 300f, 300f), new Vector2(0.5f, 0.5f), 100f);
            //add the created sprite to the display
            targetDisps[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = targSprite;
        }

        //run the UpdateNumDisp function
        updateNumDisp();
    }

    //Function to update the dictionary
    public void updateDict(string valToChange)
    {
        //subtract 1 from the targets value at the key of valToChange 
        targets[valToChange] -= 1;

        //run the UpdateNumDisp function
        updateNumDisp();
    }

    //update the numbers in the display
    private void updateNumDisp()
    {
        //iterate through the number of objects in the targets dictionary
        for (int i = 0; i < targets.Count; i++)
        {
            //update the text in the display with the value in the dictionary at the key list location for this value of i (as dictionaries aren't directly iterable)
            targetDisps[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = targets[targetDictKeys[i]].ToString();
        }
    }
}
