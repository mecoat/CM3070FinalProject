using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Level Controls")]
    private int currScore;
    public int levelNo;

    private GameObject HUD;
    private GameObject displayScore;
    [SerializeField]
    Levels currentLevel;

    private int timeForLevel;

    [SerializeField] public CustomTimer inGameTimer;


    private void Awake()
    {
        GetLevelData();

        //objectList = new List<GameObject>();
    }


    /// <summary>
    /// Gets the data of the current level through playerPrefs and finding assets
    /// </summary>
    private void GetLevelData()
    {
        int levelNum = PlayerPrefs.GetInt("LevelNum");
        string levelName = "Level" + levelNum + "Data";
        //https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html
        //string[] guids = AssetDatabase.FindAssets(levelName);

        //https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Resources.Load.html accessed 31/12/24
        var levelToLoad = Resources.Load<Levels>("Level Data/" + levelName);

        currentLevel = levelToLoad;
        levelNo = levelNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelData();

        if (timeForLevel > 0)
        {
            
            //Activate the timer related objects
            inGameTimer.gameObject.SetActive(true);
            inGameTimer.SetTimeFromSeconds(timeForLevel);
            inGameTimer.StartTimer();
            StartCoroutine(WaitForTimerRunOut());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initalize the data from the current levels data into variables
    /// </summary>
    private void InitializeLevelData()
    {
        timeForLevel = currentLevel.timer;

    }


    /// <summary>
    /// Generate the objects for the current level
    /// based on the data collected from InitializeLevelData()
    /// </summary>
    void generateObjects()
    {
    
    }


    IEnumerator WaitForTimerRunOut()
    {
        yield return new WaitForSeconds(timeForLevel);
        EndGame();
    }


    public void EndGame()
    {
        Debug.Log("Game over");
    }


}
