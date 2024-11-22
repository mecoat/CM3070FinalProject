using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorControl : MonoBehaviour
{

    [SerializeField]
    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementZ = Input.GetAxis("Vertical");
        float movementX = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(movementX, 0, movementZ) * movementSpeed * Time.deltaTime);
    }
}
