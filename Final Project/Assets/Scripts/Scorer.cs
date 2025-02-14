using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scorer : MonoBehaviour
{

    private int currScore = 0;

    [SerializeField]
    Text scoreDisplay;

    //[SerializeField]
    //GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        updateScpre(0);
    }

    // Update is called once per frame
    void Update()
    {
        //updateScpre(1);
    }


    public void updateScpre(int addVal)
    {
        currScore += addVal;
        
        string disp = "Current Score" + "\n" + currScore.ToString();

        scoreDisplay.text = disp;

    }
}
