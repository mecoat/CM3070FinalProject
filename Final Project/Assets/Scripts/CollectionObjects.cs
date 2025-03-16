using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObjects : MonoBehaviour
{

    //nolder for the object rigidbody values
    Rigidbody rb;

    //bool to hold the dropping status of the object
    private bool isDropping = true;  // set to true on spawn so that it counts if it lands in the tray on spawn

    // Start is called before the first frame update
    void Start()
    {
        //initiation of the rb variable of the rigidbody on the object
        rb = GetComponent<Rigidbody>();
    }

    //function to transfer the parent of the object to the collector on collection
    public void transferToCollector(GameObject obj)
    {
        //add the object to the collector (becoming a child of the collector)
        transform.SetParent(obj.transform, true);

        //diasble the relevant rigidbody elements so that it moves with the collector
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    //function to remove the object from the collector 
    public void dropFromCollector()
    {
        //remove the object from the collector as parent
        transform.SetParent(null);

        //re-enable the rigidbody elements to enable it to fall from the collector
        rb.isKinematic = false;
        rb.detectCollisions = true;

        //change the isDropping variable to true to indicate theat the object is dropping
        isDropping = true;
    }

    //function that run when the object enters a trigger
    private void OnTriggerEnter(Collider trigger)
    {
        //if the object is dropping...
        if (isDropping)
        {
            //if the trigger its the tray trigger
            if (trigger.name == "TrayTrigger")
            {
                //change isDropping to false to indicate that it is no longer dropping
                isDropping = false;

                //add the object into the trayObjects object
                GameObject obj = GameObject.Find("TrayObjects");
                transform.SetParent(obj.transform, true);
            }
            //otherwise, if the trigger is the Arena trigger...
            else if (trigger.name == "ArenaTrigger")
            {
                //change isDropping to false to indicate that it is no longer dropping
                isDropping = false;

                //add the object back to the Objects object
                GameObject obj = GameObject.Find("Objects");
                transform.SetParent(obj.transform, true);
            }
        }
    }

    //function to destrooy this object
    public void destroySelf()
    {
        //destroy this object
        Destroy(gameObject);
    }
}
