using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstructionsKeyListener : MonoBehaviour
{
    [SerializeField]
    Button StartGameButton;

    [SerializeField]
    Button MenuGameButton;

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

        if (Input.GetButtonDown("Fire1"))
        {
            MenuGameButton.onClick.Invoke();
        }
    }
}
