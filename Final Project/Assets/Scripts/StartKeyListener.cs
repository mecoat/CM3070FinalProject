using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartKeyListener : MonoBehaviour
{
    [SerializeField]
    Button StartGameButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartGameButton.onClick.Invoke(); 
        }

    }
}
