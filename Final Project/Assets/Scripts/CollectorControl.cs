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
    private bool hasObject = false;

    //boolean to indicate that the level has ended
    private bool endLevel = false;

      // Start is called before the first frame update
    void Start()
    {
        //initiation of the rb variable of the rigidbody on the object
        rb = GetComponent<Rigidbody>();
    }

    //fixed update runs on a set schedule depending on the frequency of the frame updates (so this runs the same speed on different devices regardless of frame rate)
    private void FixedUpdate()
    {
        //reset the velocities of movement and rotation (to prevent undesired movement, such as drift after depositing an object)
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //if the playerMove variable is true (player control is on) and endLevel is false (the level has not ended)
        if (playerMove & !endLevel)
        {
            //Get the values of the player input and assign them to variables (raw means that there is no drift when the player stops pressing the button)
            float movementZ = Input.GetAxisRaw("Vertical");
            float movementX = Input.GetAxisRaw("Horizontal");

            //create a vector based upon the playerinput values and movement speed, with a calutlation for delta time (to allow for different frame rates)
            Vector3 newPos = new Vector3(movementX, 0, movementZ) * movementSpeed * Time.deltaTime;

            //move the controller rigidbody according to the calculated vector
            rb.MovePosition(transform.position + newPos);
        }

        //if the player presses Fire2 button (Left Alt)
        if (Input.GetButtonDown("Fire2"))
        {
            //if the collector has no object...
            if (!hasObject)
            {
                //chnge playerMove to false (which stops player control)
                playerMove = false;

                //trigger the dropCollector function
                dropCollector();
            }
            //otherwise if there is an object...
            else
            {
                //drop the object
                dropObject();
            }
        }

        //if the playerMove variable is false (player control is off)
        if (!playerMove)
        {
            // if variable isDropping is true (collector is dropping)
            if (isDropping)
            {
                //create a vector with a negative y value (downwards)
                Vector3 dropPos = new Vector3(0, -5, 0) * movementSpeed * Time.deltaTime;
                //adjust the position of the collector RigidBody by adding the new variable to current location vector
                rb.MovePosition(transform.position + dropPos);
            }
            //otherwise, collector is not dropping (and needs to rise)...
            else 
            {
                //create a vector with a positive y value (upwards)
                Vector3 raisePos = new Vector3(0, 3, 0) * movementSpeed * Time.deltaTime;
                //adjust the position of the collector RigidBody by adding the new variable to current location vector
                rb.MovePosition(transform.position + raisePos);

                //if the rigid body y position is greater than or equal to 5 (home height)
                if (rb.transform.position.y >= 5)
                {
                    //move to exactly 5 high (for consistency for player)
                    rb.position = new Vector3(rb.transform.position.x, 5f, rb.transform.position.z);

                    //return control to player 
                    playerMove = true;
                }
            }
        }
    }

    //function to drop the collector
    private void dropCollector()
    {
        //remove all constraints on the rigid body
        rb.constraints = RigidbodyConstraints.None;
        //place a contrasint to prevent rotation
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        //chage isDroppping variable to true to indicate that the collector is dropping
        isDropping = true;
    }

    //when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        //if the collision is with something that has the tag of "Object" (and also isDropping (to prevent recollecting an object immediately on release))
        if (collision.gameObject.tag == "Object"  && isDropping)
        {
            //check the collector hasn't already collected something 
            if (!hasObject)
            {
                //change hasObject to true (indicates that the object is collected)
                hasObject = true;

                //run transferToCollector function on the object that has been collided with
                collision.gameObject.GetComponent<CollectionObjects>().transferToCollector(this.gameObject);

                //change isdropping to false (we do not want to the object to continue down)
                isDropping = false;
            }
        }
        //otherwise, if the collector isDropping (ie not moving sideways and colliding with walls), but not collided with an object (eg collision with floor of arena or tray)...
        else if (isDropping)
        {
            //change isdropping to false (we do not want to the object to continue down)
            isDropping = false;
        }
    }

    //function to drop the object fronm the collector
    private void dropObject()
    {
        //call dropFromCollector on the object that is a part of the collector
        this.gameObject.GetComponentInChildren<CollectionObjects>().dropFromCollector();

        //chage hasObject variable to false to indicate that the object has been dropped
        hasObject = false;

        //move to exactly 5 high (for consistency for player)
        rb.position = new Vector3(rb.transform.position.x, 5f, rb.transform.position.z);
    }

    //function to stop movement if the level is ended (to prevent movement if the player continues to press (eg with a high score)
    public void stopMovement()
    {
        //change end level to true
        endLevel = true;
    }
}

