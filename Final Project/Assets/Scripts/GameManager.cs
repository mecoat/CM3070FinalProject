using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    [SerializeField]
    Levels currentLevel;
    public int levelNo;

    [SerializeField]
    GameObject Scorer;

    [SerializeField]
    GameObject Timer;

    [SerializeField]
    GameObject targetsDisp;


    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject nextLev;

    [SerializeField]
    GameObject gameOverScore;
    [SerializeField]
    GameObject nextLevScore;

    [SerializeField]
    Dropdown inital1;
    [SerializeField]
    Dropdown inital2;
    [SerializeField]
    Dropdown inital3;

    private string playerInits = "AAA";

    private List<GameObject> spawnObjects = new List<GameObject>();
    private List<GameObject> targetObjects = new List<GameObject>();
    private int timer;
    private Dictionary<string, int> targetDict = new Dictionary<string, int>();

    private int maxObjects = 2;

    //private GameObject sceneCanvGO;
    //private GameObject targetsDisp;
    //private GameObject gameOver;
    //private GameObject nextLev;

    [SerializeField]
    GameObject Objects;

    private int highestLevel = 6;
    private int levelNum = 1;

    private void Awake()
    {
        GetLevelData();

    }

    private void GetLevelData()
    {
        //int levelNum = 1;
        //int levelNum = MainManager.Instance.getLevel();
        levelNum = MainManager.Instance.getLevel();

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

        //sceneCanvGO = GameObject.Find("sceneCanvas");

        //GameObject.Find("Timer").GetComponent<Timer>().setTimer(timer);
        Timer.GetComponent<Timer>().setTimer(timer);

        createTargetDict();

        //targetsDisp = sceneCanvGO.transform.Find("Targets").gameObject;

        targetsDisp.GetComponent<TargetDisp>().setTargets(targetDict);

        //gameOver = sceneCanvGO.transform.Find("GameOver").gameObject;


        //nextLev = sceneCanvGO.transform.Find("NextLevel").gameObject;

        Scorer.GetComponent<Scorer>().updateScpre(MainManager.Instance.getScore());
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
                //int arenaSection = Random.Range(0, 3);

                //Debug.Log(arenaSection);

                //float randX = Random.Range(-2f, 2f);
                //float randZ = Random.Range(-2f, 2f);

                //if (arenaSection == 0) // far left corner
                //{
                //  randX = randX - 2;

                //randZ = randZ + 2;
                //}
                //else if (arenaSection == 1) // near left corner
                //{
                //  randX = randX - 2;

                //randZ = randZ - 2;
                //}
                //else if (arenaSection == 2) // near right corner
                //{
                //  randX = randX + 2;

                //randZ = randZ - 2;
                //}

                (float randX, float randZ) = getSpawnLoc();

                Vector3 randLoc = new Vector3(randX, 2, randZ);
                //Debug.Log(randLoc);

                Quaternion randRot = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                //GameObject spawnObject = Instantiate(spawnObjects[i], randLoc, randRot, GameObject.Find("Objects").transform);
                GameObject spawnObject = Instantiate(spawnObjects[i], randLoc, randRot, Objects.transform);
                //and change its name (to be sure it matches the above check)
                spawnObject.name = spawnObjects[i].name;
            }
        }
    }

    private (float, float) getSpawnLoc()
    {
        float randX = Random.Range(-4f, 4f);
        float randZ = Random.Range(-4f, 4f) ;

        if (randX > -0.5 && randX <= 4 && randZ > 1.5 && randZ <= 4)
        {
            (randX, randZ) = getSpawnLoc();
        }

        return (randX, randZ);
    }

    public int getMaxObjects()
    {
        return maxObjects;
    }

    public void endGame(bool trayFull = false, bool targetsMet = false)
    {

        int playerScore;

        //when the function is called if the tray is full (with non-matching objects)...
        if (trayFull)
        {
            PlaySounds.SoundInstance.playLoseSound();

            //Debug.Log("Game over (from Manager)");

            //get the scene canvas
            //Transform sceneCanv = sceneCanvGO.transform;

            //string gameOverName = "Game Over";

            //check if the canvas already contains the gameover object
            //if (!sceneCanv.Find(gameOverName))
            //{
            //if it doesn't, add the game over object
            //  GameObject gameOverCanv = Instantiate(gameOver, sceneCanv);
            //and change its name (to be sure it matches the above check)
            //gameOverCanv.name = gameOverName;
            //}
            gameOver.SetActive(true);

            playerScore = Scorer.GetComponent<Scorer>().getScore();

            gameOverScore.GetComponent<Text>().text = "Your Score : " + playerScore;

            //Debug.Log("trayfull called");

            //MainManager.Instance.addToHighScore("MCC", playerScore);

            //reset level to 1
            MainManager.Instance.resetLevel();

            //reset player score in Main Manager
            MainManager.Instance.resetScore();

        }
        //win level
        else if (targetsMet)
        {
            //Debug.Log("All targets met");

            //get the scene canvas
            //Transform sceneCanv = sceneCanvGO.transform;

            //string nextLevName = "Next Level";

            //check if the canvas already contains the nextLev object
            //if (!sceneCanv.Find(nextLevName))
            //{
            //if it doesn't, add the game over object
            //  GameObject nextLevCanv = Instantiate(nextLev, sceneCanv);
            //and change its name (to be sure it matches the above check)
            //and changedeb its name (to be sure it matches the above check)
            //nextLevCanv.name = nextLevName;
            //}

            int leftoverTime = Timer.GetComponent<Timer>().getTimer();
            Scorer.GetComponent<Scorer>().updateScpre(leftoverTime);

            playerScore = Scorer.GetComponent<Scorer>().getScore();
            nextLevScore.GetComponent<Text>().text = "Your Score : " + playerScore;

            if (levelNum >= highestLevel)
            {
                gameOver.SetActive(true);

                playerScore = Scorer.GetComponent<Scorer>().getScore();

                gameOverScore.GetComponent<Text>().text = "Your Score : " + playerScore;

                //Debug.Log("trayfull called");

                //MainManager.Instance.addToHighScore("MCC", playerScore);

                //reset level to 1
                MainManager.Instance.resetLevel();

                //reset player score in Main Manager
                MainManager.Instance.resetScore();
            } 
            else
            {
                nextLev.SetActive(true);

                //update level to nect level
                MainManager.Instance.moveLevel();

                //update total player score
                MainManager.Instance.updateScore(playerScore);
            }

            //nextLev.SetActive(true);

            //update level to nect level
            //MainManager.Instance.moveLevel();

            //update total player score
            //MainManager.Instance.updateScore(playerScore);
        }

        //stop the timer
        //GameObject.Find("Timer").GetComponent<Timer>().stopTimer();
        Timer.GetComponent<Timer>().stopTimer();

        

    }

    public void checkMatch(string matchName)
    {
        //add 20 for a match
        Scorer.GetComponent<Scorer>().updateScpre(20);

        for (int i = 0; i < targetObjects.Count; i++)
        {
            if (matchName == targetObjects[i].name)
            {
                targetObjects.RemoveAt(i);

                targetsDisp.GetComponent<TargetDisp>().updateDict(matchName);

                //add additional 30 for a target match
                Scorer.GetComponent<Scorer>().updateScpre(30);

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

    public void returnToStart()
    {
        int playerScore = Scorer.GetComponent<Scorer>().getScore();

        MainManager.Instance.addToHighScore(playerInits, playerScore);

        //load the start scene
        SceneManager.LoadScene("StartScene");
    }

    public void reloadScene()
    {
        //load the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void updatePlayerInits(int indToChange)
    {
        string newInit = "";

        if (indToChange == 0)
        {
            newInit = inital1.options[inital1.value].text;
        }
        else if (indToChange == 1)
        {
            newInit = inital2.options[inital2.value].text;
        }
        else if (indToChange == 2)
        {
            newInit = inital3.options[inital3.value].text;
        }

        playerInits = playerInits.Remove(indToChange, 1).Insert(indToChange, newInit);
        Debug.Log(playerInits);

    }
}
