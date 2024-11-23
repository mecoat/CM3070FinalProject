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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }

        Debug.Log(hasCollected);
    }
}
