using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScores : MonoBehaviour
{
    [SerializeField]
    GameObject leftDisplay;
    [SerializeField]
    GameObject rightDisplay;



    // Start is called before the first frame update
    void Start()
    {
        List<(string, int)> highScores = MainManager.Instance.getHighScores();

        leftDisplay.GetComponent<Text>().text = createHighScores(0, highScores);
        rightDisplay.GetComponent<Text>().text = createHighScores(5, highScores);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string createHighScores(int startVal, List<(string, int)> fullListScores)
    {
        string newText = "";

        int listLength = fullListScores.Count;

        for (int i = startVal; i < (startVal + 5); i++)
        {
            if (listLength > i)
            {
                newText += fullListScores[i].Item1 + " " + fullListScores[i].Item2.ToString();
            }
            else
            {
                newText += "MCC 42";
            }

            if (i < startVal + 4)
            {
                newText += "\n";
            }
        }

        newText.Remove(newText.Length -2);

        return newText;
    }
}
