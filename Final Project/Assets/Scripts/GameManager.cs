using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{

    [SerializeField]
    Levels currentLevel;
    public int levelNo;

    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject nextLev;

    private List<GameObject> spawnObjects;
    private List<GameObject> targetObjects;
    private int timer;
    private Dictionary<string, int> targetDict = new Dictionary<string, int>();

    private int maxObjects = 2;

    private GameObject sceneCanvGO;
    private GameObject targetsDisp;

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

        generateObjects();

        sceneCanvGO = GameObject.Find("sceneCanvas");

        GameObject.Find("Timer").GetComponent<Timer>().setTimer(timer);

        createTargetDict();

        targetsDisp = GameObject.Find("Targets");

        targetsDisp.GetComponent<TargetDisp>().setTargets(targetDict);

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

        //Debug.Log("timer = " + timer);
        //Debug.Log("Spawn = " + spawnObjects);
        //Debug.Log("target = " + targetObjects);
   
    }

    private void generateObjects()
    {
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            for (int j = 0; j < maxObjects; j++)
            {
                int arenaSection = Random.Range(0, 3);

                //Debug.Log(arenaSection);

                float randX = Random.Range(-2f, 2f);
                float randZ = Random.Range(-2f, 2f);

                if (arenaSection == 0) // far left corner
                {
                    randX = randX - 2;

                    randZ = randZ + 2;
                }
                else if (arenaSection == 1) // near left corner
                {
                    randX = randX - 2;

                    randZ = randZ - 2;
                }
                else if (arenaSection == 2) // near right corner
                {
                    randX = randX + 2;

                    randZ = randZ - 2;
                }


                Vector3 randLoc = new Vector3(randX, 2, randZ);
                //Debug.Log(randLoc);

                Quaternion randRot = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                GameObject spawnObject = Instantiate(spawnObjects[i], randLoc, randRot, GameObject.Find("Objects").transform);
                //and change its name (to be sure it matches the above check)
                spawnObject.name = spawnObjects[i].name;
            }
        }
    }

    public int getMaxObjects()
    {
        return maxObjects;
    }

    public void endGame(bool trayFull = false, bool targetsMet = false)
    {
        //when the function is called if the tray is full (with non-matching objects)...
        if (trayFull)
        {
            //Debug.Log("Game over (from Manager)");

            //get the scene canvas
            Transform sceneCanv = sceneCanvGO.transform;

            string gameOverName = "Game Over";

            //check if the canvas already contains the gameover object
            if (!sceneCanv.Find(gameOverName))
            {
                //if it doesn't, add the game over object
                GameObject gameOverCanv = Instantiate(gameOver, sceneCanv);
                //and change its name (to be sure it matches the above check)
                gameOverCanv.name = gameOverName;
            }
        }
        else if (targetsMet)
        {
            //Debug.Log("All targets met");

            //get the scene canvas
            Transform sceneCanv = sceneCanvGO.transform;

            string nextLevName = "Next Level";

            //check if the canvas already contains the nextLev object
            if (!sceneCanv.Find(nextLevName))
            {
                //if it doesn't, add the game over object
                GameObject nextLevCanv = Instantiate(nextLev, sceneCanv);
                //and change its name (to be sure it matches the above check)
                nextLevCanv.name = nextLevName;
            }
        }

    }

    public void checkMatch(string matchName)
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            if (matchName == targetObjects[i].name)
            {
                targetObjects.RemoveAt(i);

                targetsDisp.GetComponent<TargetDisp>().updateDict(matchName);
                break;
            }

        }

        if (targetObjects.Count == 0)
        {
            endGame(false, true);
        }
    }

    //public int setTimerDisp()
    //{//
      //  Debug.Log("getTimer : " + timer);

        //return timer;
    //}

    private void createTargetDict()
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            string targetName = targetObjects[i].name;
            //Debug.Log(targetName);

            if (targetDict.ContainsKey(targetName))
            {
                targetDict[targetName] = targetDict[targetName] + 1;
            } 
            else
            {
                targetDict.Add(targetName, 1);
            }
        }
    }
}
