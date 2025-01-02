using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayChecker : MonoBehaviour
{
    //holder for maximum noumber of objects in the tray
    private int maxObjects = 2;

    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log(this.transform.GetChild(i).gameObject.name);
                Debug.Log(this.transform.GetChild(i - 1).gameObject.name);

                if (this.transform.GetChild(i).gameObject.name != this.transform.GetChild(i - 1).gameObject.name)
                {
                    match = false;
                }
            }

            if (match == false )
            {
                Debug.Log("Game over");
            } else if (match == true)
            {
                Debug.Log("Successful match");
            }
        }
    }
}
