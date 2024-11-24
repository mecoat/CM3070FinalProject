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

    private GameObject tray;
    private Vector3 trayLoc;
    private Vector3 trayPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        tray = GameObject.Find("Tray");
        trayLoc = tray.transform.position;
        trayPos = new Vector3(trayLoc.x, 0, trayLoc.y) * movementSpeed * Time.deltaTime;
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
                    if (!hasCollected)
                    {
                        playerMove = true;
                        Debug.Log("return control to player");
                    }
                    else
                    {
                        Debug.Log("taking object to tray");
                        rb.MovePosition(transform.position + trayPos);

                    }
                }
            }
        }
    }

    private void dropCollector()
    {
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
