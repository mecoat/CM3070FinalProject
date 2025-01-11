using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetDisp : MonoBehaviour
{
    private Dictionary <string,int> targets = new Dictionary<string, int>();

    private GameObject targetHolder;
    private List<GameObject> targetDisps = new List<GameObject>();
    //private GameObject target1;
    //private GameObject target2;
    //private GameObject target3;
    //private GameObject target4;
    private List <string> targetDictKeys;


    // Start is called before the first frame update
    void Start()
    {
        targetHolder = GameObject.Find("Targets");

        Debug.Log("hoder1" + targetHolder.transform.GetChild(2).gameObject.name);
        
        targetDisps.Add(targetHolder.transform.GetChild(2).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(3).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(4).gameObject);
        targetDisps.Add(targetHolder.transform.GetChild(5).gameObject);

        //target1 = targetHolder.transform.GetChild(2).gameObject;
        //target2 = targetHolder.transform.GetChild(3).gameObject;
        //target3 = targetHolder.transform.GetChild(4).gameObject;
        //target4 = targetHolder.transform.GetChild(5).gameObject;

        //for (int i = 0; i < targets.Count; i++)
        //{
            //make the display appear
          //  targetDisps[i].SetActive(true);
           // Debug.Log(targetDisps[i].name);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTargets(Dictionary<string, int> inpTargets)
    {
        targets = inpTargets;
        Debug.Log(targets);

        targetDictKeys = new List<string>(targets.Keys);

        setupDisplay();
    }

    public void setupDisplay()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            //make the display appear
            targetDisps[i].SetActive(true);
            Debug.Log(targetDisps[i].name);

            targetDisps[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = targets[targetDictKeys[i]].ToString();

            Sprite targSprite = Sprite.Create(Resources.Load<Texture2D>("TargetImages/" + targetDictKeys[i] + ".png"),
                                                                                                            new Rect(0f, 0f, 30f, 30f), new Vector2(0.5f, 0.5f), 0f);
            Debug.Log(targSprite);

            targetDisps[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Sprite.Create( Resources.Load<Texture2D>("TargetImages/" + targetDictKeys[i] + ".png"), 
                                                                                                            new Rect (0f, 0f, 30f, 30f), new Vector2(0.5f, 0.5f), 0f);

        }
    }
}
