﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Levels currentLevel;
    public int levelNo;

    private List<GameObject> spawnObjects;
    private List<GameObject> targetObjects;
    private int timer;


    private void Awake()
    {
        GetLevelData();

    }

    private void GetLevelData()
    {
        int levelNum = 1;
        string levelName = "Level" + levelNum + "Data";
  
        //https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Resources.Load.html accessed 3/1/25
        var levelToLoad = Resources.Load<Levels>("Level Data/" + levelName);

        currentLevel = levelToLoad;
        levelNo = levelNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeLevelData()
    {

        spawnObjects = currentLevel.spawnObjects;
        targetObjects = currentLevel.targetObjects;
        timer = currentLevel.timer;

        Debug.Log("timer = " + timer);
        Debug.Log("Spawn = " + spawnObjects);
        Debug.Log("target = " + targetObjects);
   
    }
}