using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorControl : MonoBehaviour
{
    //nolder for the collector rigidbody values
    Rigidbody rb;

    //movement speed variable (adjustable from within Unity)
    [SerializeField]
    float movementSpeed;

    //boolean to indicate if control of collector is wit the player (true) or not (false)
    private bool playerMove = true;

    //boolean to indicate if the collector is dropping from its position
    private bool isDropping = false;

    //boolean to indiicate if the collector as collected an object
    private bool hasCollected = false;

    //boolean to indicate the object has been deposited in the tray
    private bool hasDeposited = false;

    //values to identify and locate the tray
    private GameObject tray;
    private Vector3 trayLoc;
    private Vector3 trayPos;

      // Start is called before the first frame update
    void Start()
    {
        //initiation of the rb variable of the rigidbody on the object
        rb = GetComponent<Rigidbody>();

        //initiate the tray values by finding the tray...
        tray = GameObject.Find("Tray");
        //...getting the tray location...
        trayLoc = tray.transform.position;
        //...and changing it to a more usable movement value
        trayPos = new Vector3(tray.transform.position.x, 0, tray.transform.position.z) * movementSpeed * Time.deltaTime; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        //if the playerMove variable is true (player control is on)
        if (playerMove)
        {
            //Get the values of the player input and assign them to variables (raw means that there is no drift when the player stops pressing the button)
            float movementZ = Input.GetAxisRaw("Vertical");
            float movementX = Input.GetAxisRaw("Horizontal");

            //create a vector based upon the playerinput values and movement speed, with a calutlation for delta time (to allow for different frame rates)
            Vector3 newPos = new Vector3(movementX, 0, movementZ) * movementSpeed * Time.deltaTime;

            //move the controller rigindbody according to the calculated vector
            rb.MovePosition(transform.position + newPos);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerMove = false;

            dropCollector();
        }

        if (!playerMove)
        {
            if (isDropping)
            {
                Vector3 dropPos = new Vector3(0, -5, 0) * movementSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + dropPos);
            } 
            else if (hasCollected || !isDropping)
            {
                Vector3 raisePos = new Vector3(0, 3, 0) * movementSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + raisePos);

                if (rb.transform.position.y >= 9)
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionY;

                    if (!hasCollected)
                    {
                        playerMove = true;
                        Debug.Log("return control to player");
                    }
                    else if (!hasDeposited)
                    {
                        if (rb.transform.position.x <= trayLoc.x  && rb.transform.position.z <= trayLoc.z)
                        {
                            Debug.Log("taking object to tray");
                            rb.MovePosition(transform.position + trayPos);

                        }
                        else
                        {
                            hasDeposited = true;
                            Debug.Log("returning to arena");

                            rb.MovePosition(transform.position - trayPos);

                        }


                    }
                    else
                    {
                        playerMove = true;
                        Debug.Log("return control to player");
                        hasDeposited = false;
                        hasCollected = false;


                    }
                }
            }
        }
    }

    private void dropCollector()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Dropping collector");
        isDropping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Object")
        {
            hasCollected = true;
            isDropping = false;
            Debug.Log("collecting object");
        }
        else if (isDropping)
        {
            isDropping = false;
            Debug.Log("Dropped, no object");
        }

    }
}
