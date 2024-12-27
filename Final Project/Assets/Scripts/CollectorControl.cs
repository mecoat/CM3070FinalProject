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

    //boolean to indicate the object has been deposited in the tray
    //private bool hasDeposited = false;

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
                dropObject();
            }

        }

        //if the playerMove variable of false (player control is off)
        if (!playerMove)
        {
            // if variable isDropping is true (collecor is dropping)
            if (isDropping)
            {
                //create a vector with a negative y value (downwards)
                Vector3 dropPos = new Vector3(0, -5, 0) * movementSpeed * Time.deltaTime;
                //adjust the position of the collector RigidBody by adding the new variable to current location vector
                rb.MovePosition(transform.position + dropPos);
            }
            //otherwise, if either hasObject (Collector touched an Object) or isn't dropping (touched the floor)
            else if (hasObject || !isDropping)
            {
                //create a vector with a positive y value (upwards)
                Vector3 raisePos = new Vector3(0, 3, 0) * movementSpeed * Time.deltaTime;
                //adjust the position of the collector RigidBody by adding the new variable to current location vector
                rb.MovePosition(transform.position + raisePos);

                //if the rigid body y position is greater than or equal to 9 (home height)
                if (rb.transform.position.y >= 9)
                {
                    //move to exactly 9 high (for consistency for player)
                    rb.position = new Vector3(rb.transform.position.x, 9, rb.transform.position.z);

                    //ensure that collector is properly returned to player control with no drift or rotation...
                    //prevent the Rigid body from moving at all (reset anything that may have been added to the collector motion/rotation)
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    //remove all constrainst (so the player can have movement again)
                    rb.constraints = RigidbodyConstraints.None;
                    //prevent the Rigid body from rotating (so that an uneven cintact will not turn the collector)
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    //prevent the Rigid body from moving more in y direction (hold the collector at the correct height (and prevent gravity from dropping it))
                    rb.constraints = RigidbodyConstraints.FreezePositionY;

                    //return control to player 
                    playerMove = true;

                    //if it hasn't collected
                    //if (!hasObject)
                    //{
                    //change playerMove to true (return control to player)
                    //  playerMove = true;
                    //output to console to show what's happening)
                    //Debug.Log("return control to player");
                    //}
                    //otherwise if hasDeposited is false (there is an object on the collector)
                    //else if (!hasDeposited)
                    //{
                    //if the position is not at the try yet
                    //if (rb.transform.position.x <= trayLoc.x  && rb.transform.position.z <= trayLoc.z)
                    //{
                    //  //output to console to show what's happening)
                    //Debug.Log("taking object to tray");
                    ////move collector towards the tray
                    //rb.MovePosition(transform.position + trayPos);
                    //}
                    //otherwise ... (it's reached the tray)
                    //else
                    //{
                    //  //change has depostied to true (as objet will be put into try
                    //hasDeposited = true;
                    ////more to add here to actually deposit the object

                    ////output to console to show what's happening)
                    //Debug.Log("returning to arena");
                    ////move away from the tray towards earlier position
                    //rb.MovePosition(transform.position - trayPos);
                    //}
                    //}
                    //otherwise... (hasDeposited is true)
                    //else
                    //{
                    //change playerMove to true (return control to player)
                    //  playerMove = true;
                    //output to console to show what's happening)
                    //Debug.Log("return control to player");
                    //change hasDeposited to false (revert to default
                    //hasDeposited = false;
                    //change has collected to false (refert to default)
                    //hasObject = false;
                    //}
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

        //output to console to show what's happening)
        Debug.Log("Dropping collector");
        //chage isDroppping variable to true to indicate that the collector is dropping
        isDropping = true;
    }

    //when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        //output the tag of the element collided with to the console
        Debug.Log(collision.gameObject.tag);

        //if the collision with with something that has the tag of "Object"
        if (collision.gameObject.tag == "Object")
        {
            //change hasObject to true (indicates that the object is collected)
            hasObject = true;
            //More to add here to actually collect the object

            //change isdropping to false (we do not want to the object to continue down)
            isDropping = false;
            //output to console to show what's happening)
            Debug.Log("collecting object");
        }
        //otherwise, if the collector isDropping (ie not moving sideways and colliding with walls...
        else if (isDropping)
        {
            //change isdropping to false (we do not want to the object to continue down)
            isDropping = false;
            //output to console to show what's happening)
            Debug.Log("Dropped, no object");
        }
    }

    //function to drop the object fronm the collector
    private void dropObject()
    {

        //output to console to show what's happening)
        Debug.Log("Dropping object");

        // code here to drop the object


        //chage hasObject variable to false to indicate that the object has been dropped
        hasObject = false;
    }
}
