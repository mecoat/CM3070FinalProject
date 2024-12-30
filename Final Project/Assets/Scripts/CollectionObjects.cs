using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionObjects : MonoBehaviour
{

    //nolder for the object rigidbody values
    Rigidbody rb;

    public void transferToCollector(GameObject obj)
    {
        transform.SetParent(obj.transform, true);
        transform.localPosition = new Vector3(0f, -1.5f, 0f);

        rb.isKinematic = true;
        rb.detectCollisions = false;


        //transform.parent = obj.transform;
        //transform.localPosition = new Vector3(0f, 1f, 0.05f);

        //turn off gravity for the object
        //rb.useGravity = false;

        //prevent the Rigid body from moving at all (reset anything that may have been added to the collector motion/rotation)
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //remove all constrainst (so the player can have movement again)
        //rb.constraints = RigidbodyConstraints.None;
        //prevent the Rigid body from rotating (so that an uneven cintact will not turn the collector)
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
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
}
