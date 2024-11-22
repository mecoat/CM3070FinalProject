using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorControl : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float movementSpeed;

    //[SerializeField]
    private bool playerMove = true;

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
        }
    }
}
