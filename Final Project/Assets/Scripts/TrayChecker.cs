using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayChecker : MonoBehaviour
{
    //holder for maximum noumber of objects in the tray
    private int maxObjects; // = 2;

    // Start is called before the first frame update
    void Start()
    {
        maxObjects = GameObject.Find("Manager").GetComponent<GameManager>().getMaxObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.childCount);

        //holder for count of children of object
        int childCount = this.transform.childCount;

        //check if the number of children is the maximum (if less - not gonna match, should not get to more)
        if (childCount == maxObjects)
        {
            bool match = true;

            for (int i = 1; i < maxObjects; i++)
            {

                if (this.transform.GetChild(i).gameObject.name != this.transform.GetChild(i - 1).gameObject.name)
                {
                    match = false;
                }
            }

            if (match == false )
            {
                //end game - needs to be replaced to actually end the game
                //Debug.Log("Game over");

                GameObject.Find("Manager").GetComponent<GameManager>().endGame(true);
            } 
            else if (match == true)
            {
                //Debug.Log("Successful match");

                //iterate through the matched objects
                for (int i = 0; i < maxObjects; i++)
                {
                    //log the match - needs to be replaced with working code to acually log this
                    Debug.Log("logging : " + this.transform.GetChild(i).gameObject.name);

                    //remmove from scene
                    this.transform.GetChild(i).gameObject.GetComponent<CollectionObjects>().destroySelf();
                }
            }
        }
    }

    
}
