using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObjects : MonoBehaviour
{

    //nolder for the object rigidbody values
    Rigidbody rb;

    //bool to hold the dropping status of the object
    private bool isDropping = false;

    public void transferToCollector(GameObject obj)
    {
        //add the object to the collector (becoming a child of the collector)
        transform.SetParent(obj.transform, true);
        
        //transform.localPosition = new Vector3(0f, -1.5f, 0f);

        //diasble the relevant rigidbody elements so that it moves with the collector
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }

    public void dropFromCollector()
    {
        //remove the object from the collector as parent
        transform.SetParent(null);

        //re-enable the rigidbody to enable it to fall from the collector
        rb.isKinematic = false;
        rb.detectCollisions = true;

        isDropping = true;
        Debug.Log("object dropping = " + isDropping);
    }



    // Start is called before the first frame update
    void Start()
    {
        //initiation of the rb variable of the rigidbody on the object
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log(trigger);
        if (isDropping)
        {
            if (trigger.name == "TrayTrigger")
            {
                Debug.Log("in the tray");

                isDropping = false;
                Debug.Log("object dropping = " + isDropping);

                //add the object back to the Objects object
                GameObject obj = GameObject.Find("TrayObjects");
                transform.SetParent(obj.transform, true);
            }
            else if (trigger.name == "ArenaTrigger")
            {
                Debug.Log("in the arena");

                isDropping = false;
                Debug.Log("object dropping = " + isDropping);

                //add the object back to the Objects object
                GameObject obj = GameObject.Find("Objects");
                transform.SetParent(obj.transform, true);
            }
        }
        
    }
}
