using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDisp : MonoBehaviour
{
    private Dictionary <string,int> targets;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTargets(Dictionary<string, int> inpTargets)
    {
        targets = inpTargets;
        Debug.Log(targets);
    }
}
