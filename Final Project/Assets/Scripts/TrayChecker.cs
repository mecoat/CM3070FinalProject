using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayChecker : MonoBehaviour
{
    //holder for maximum noumber of objects in the tray
    private int maxObjects; // = 2;

    //the game manager so the necessary game info can be 
    [SerializeField]
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        //initialise the maxObjects variable with the number from the GameManager
        maxObjects = manager.getMaxObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //holder for count of children of object
        int childCount = this.transform.childCount;

        //check if the number of children is the maximum (if less - not gonna match, should not get to more)
        if (childCount == maxObjects)
        {
            //declare and initiailise a variable to see if there's a match or not (set to true because we set it to fales if matches are not correct)
            bool match = true;

            //ierate through all objects (missing the first one, because it has nothing before it to check against)
            for (int i = 1; i < maxObjects; i++)
            {
                //check if the object name doesn't match the object name of the object before in the list of children...
                if (this.transform.GetChild(i).gameObject.name != this.transform.GetChild(i - 1).gameObject.name)
                {
                    //change the value of the match variable to false
                    match = false;
                }
            }

            //check if match is false (player has placed non-matching objects into the tray)
            if (match == false )
            {
                //call endgame function in manager
                manager.endGame(true, false);

                //iterate through the tray objects
                for (int i = 0; i < maxObjects; i++)
                {
                    //remmove from scene
                    this.transform.GetChild(i).gameObject.GetComponent<CollectionObjects>().destroySelf();
                }
            } 
            //check if match is trae (player has placed matching objects into the tray)
            else if (match == true)
            {
                //provide the information to checkMatch function in game manger to determine if the successful match is a target match or not and process the match
                manager.checkMatch(this.transform.GetChild(0).gameObject.name);

                //iterate through the matched objects
                for (int i = 0; i < maxObjects; i++)
                {
                    //remmove from scene
                    this.transform.GetChild(i).gameObject.GetComponent<CollectionObjects>().destroySelf();
                }
                
                //play the successful match sound
                PlaySounds.SoundInstance.playMatchSound();
            }
        }
    }
}
