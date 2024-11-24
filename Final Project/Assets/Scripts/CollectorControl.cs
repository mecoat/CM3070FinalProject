using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorControl : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float movementSpeed;

    private bool playerMove = true;

    private bool isDropping = false;

    private bool hasCollected = false;

    private bool hasDeposited = false;

    private GameObject tray;
    private Vector3 trayLoc;
    private Vector3 trayPos;

  


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        tray = GameObject.Find("Tray");
        trayLoc = tray.transform.position;
        trayPos = new Vector3(tray.transform.position.x, 0, tray.transform.position.z) * movementSpeed * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        float movementZ = Input.GetAxisRaw("Vertical");
        float movementX = Input.GetAxisRaw("Horizontal");

        Vector3 newPos = new Vector3(movementX, 0, movementZ) * movementSpeed * Time.deltaTime;

        if (playerMove)
        {
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
