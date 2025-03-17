using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    //holder for current level
    private Levels currentLevel;
    //current level numbrr
    public int levelNo;

    //the score display
    [SerializeField]
    GameObject Scorer;

    //the timer display
    [SerializeField]
    GameObject Timer;

    //the target display
    [SerializeField]
    GameObject targetsDisp;

    //the game over display
    [SerializeField]
    GameObject gameOver;
    //the end of game display
    [SerializeField]
    GameObject gameEnd;
    //the next level display
    [SerializeField]
    GameObject nextLev;

    //the game over score field
    [SerializeField]
    GameObject gameOverScore;
    //the end game score field
    [SerializeField]
    GameObject gameEndScore;
    //the xext level score field
    [SerializeField]
    GameObject nextLevScore;

    //the game over button
    [SerializeField]
    Button gameOverButton;
    //the next level button
    [SerializeField]
    Button nextLevButton;
    //the game end button
    [SerializeField]
    Button gameEndButton;

    //the hgh score input section
    [SerializeField]
    GameObject highScoreInput;

    //the collector
    [SerializeField]
    GameObject collector;

    //string of the player initials set to a default AAA
    private string playerInits = "AAA";

    //a list for the spawn objects 
    private List<GameObject> spawnObjects = new List<GameObject>();
    //a list for the target objects
    private List<GameObject> targetObjects = new List<GameObject>();
    //a holder for the timer
    private int timer;
    //a Dictionary of the targets
    private Dictionary<string, int> targetDict = new Dictionary<string, int>();

    //integer to hold the number of objects that are to be matched (set to 2 so matches are pairs)
    private int maxObjects = 2;

    //the objects gameobject in the game
    [SerializeField]
    GameObject Objects;

    //integr to indicate the highest number of levels there are (so checks can be mage and the game doesn't go to a level that doesn't exist)
    private int highestLevel = 6;

    //level number
    private int levelNum = 1;

    //runs when created
    private void Awake()
    {
        //get the level data
        GetLevelData();
    }

    //function to get the level information from the Main Manager instance
    private void GetLevelData()
    {
        //set the level number to the level from Main manager
        levelNum = MainManager.Instance.getLevel();

        //create the level (file) name from the level number
        string levelName = "Level" + levelNum + "Data";
  
        //setup the level to load
        //https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Resources.Load.html accessed 3/1/25
        var levelToLoad = Resources.Load<Levels>("Level Data/" + levelName);

        //set the current level as the level to load
        currentLevel = levelToLoad;
        //update levelNo
        levelNo = levelNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialise the level data from the function
        InitializeLevelData();

        //run the generate objects function
        generateObjects();

        //set tho simer in the scene to the value of the time declared in the level data
        Timer.GetComponent<Timer>().setTimer(timer);

        //run the function to create the target dictionary
        createTargetDict();

        //set the targets in the target object from the dictionary created
        targetsDisp.GetComponent<TargetDisp>().setTargets(targetDict);

        //update the score from the main manager instance (so it's not reset to 0 each level)
        Scorer.GetComponent<Scorer>().updateScpre(MainManager.Instance.getScore());
    }

    // Update is called once per frame
    void Update()
    {
        //if the fire2 button (left alt) is pressed...
        if (Input.GetButtonDown("Fire2"))
        {
            //if the game over object is active...
            if (gameOver.activeInHierarchy)
            {
                //invoke the fuction that's set to run when the game over button is clicked
                gameOverButton.onClick.Invoke();
            } 
            //otherwise, if the next level object is active...
            else if (nextLev.activeInHierarchy)
            {
                //invoke the function that's set to run when the next level button is clicked
                nextLevButton.onClick.Invoke();
            }
            //otherwise, if the end of game object is active...
            else if (gameEnd.activeInHierarchy)
            {
                //invoke the function that's set to run when the game end button is clicked
                gameEndButton.onClick.Invoke();
            }
        }
    }

    //function to initialise the level data
    private void InitializeLevelData()
    {
        //set spawn objects from the current level data
        spawnObjects = currentLevel.spawnObjects;
        //set target objects from the current leveldata
        targetObjects = currentLevel.targetObjects;
        //set the timer from the current level data
        timer = currentLevel.timer;
    }

    //spawn the objects
    private void generateObjects()
    {
        //ierate through the spanw objects list
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            //run for the number in Max objects (so the right number are spawned
            for (int j = 0; j < maxObjects; j++)
            {
                //set randX and randZ from the getSpawnLoc function
                (float randX, float randZ) = getSpawnLoc();

                //create a vector for the spawn location based on the rndX, randZ and a set value for Y (so that they all spawn at the same height
                Vector3 randLoc = new Vector3(randX, 2, randZ);

                //create a random rotation value
                Quaternion randRot = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                //create the right object from the list, at the randomised location, with the randomised rotation, and add as a child to the Objects gameobject
                GameObject spawnObject = Instantiate(spawnObjects[i], randLoc, randRot, Objects.transform);
                //and change its name (to be sure it matches the Name check)
                spawnObject.name = spawnObjects[i].name;
            }
        }
    }

    //function to give a spawn location
    private (float, float) getSpawnLoc()
    {
        //create a randomised value for x and z values
        float randX = Random.Range(-4f, 4f);
        float randZ = Random.Range(-4f, 4f) ;

        //if the x or z values are within the tray location (or near)
        if (randX > -0.5 && randX <= 4 && randZ > 1.5 && randZ <= 4)
        {
            //run this function again until it isn't
            (randX, randZ) = getSpawnLoc();
        }

        //return the x and z locations
        return (randX, randZ);
    }

    //function to say the number of objectsto be matched
    public int getMaxObjects()
    {
        //returns the maxObjects value
        return maxObjects;
    }

    //function to end the game/scene
    public void endGame(bool trayFull = false, bool targetsMet = false)
    {
        //stop the movement on the collector due to the end of the level
        collector.GetComponent<CollectorControl>().stopMovement();

        //holder for the player score
        int playerScore;

        //when the function is called if the tray is full (with non-matching objects), or when time runs out...
        if (trayFull)
        {
            //play the lose sound from the playsounds object
            PlaySounds.SoundInstance.playLoseSound();

            //set the game over object to active (so it appears to the player
            gameOver.SetActive(true);

            //set the player score object to the current player score from the scorer object
            playerScore = Scorer.GetComponent<Scorer>().getScore();

            //display the score on the game over object
            gameOverScore.GetComponent<Text>().text = "Your Score : " + playerScore;

            //if the player score is higher than the lowest score in the high score board
            if (playerScore > MainManager.Instance.getLowestHighScore())
            {
                //set the high score input element to active (visible to the player)
                highScoreInput.SetActive(true);
            }

            //reset level to 1
            MainManager.Instance.resetLevel();

            //reset player score in Main Manager
            MainManager.Instance.resetScore();

        }
        //otherwise, if the targetsMet is true - player won level...
        else if (targetsMet)
        {
            //get the about of left over time
            int leftoverTime = Timer.GetComponent<Timer>().getTimer();
            //add the left over time to the score (1 sec = 1 point)
            Scorer.GetComponent<Scorer>().updateScpre(leftoverTime);

            //get the player score
            playerScore = Scorer.GetComponent<Scorer>().getScore();
            
            //if the level number is higher than or equal to the highest level (last level is completed)
            if (levelNum >= highestLevel)
            {
                //activate the end of gaae object
                gameEnd.SetActive(true);

                //set the game ond score object to include the player score
                gameEndScore.GetComponent<Text>().text = "Your Score : " + playerScore;

                //if the player score is higher than the lowest score in the high score board
                if (playerScore > MainManager.Instance.getLowestHighScore())
                {
                    //set the high score input element to active (visible to the player)
                    highScoreInput.SetActive(true);
                }

                //reset level to 1
                MainManager.Instance.resetLevel();

                //reset player score in Main Manager
                MainManager.Instance.resetScore();
            } 
            //otherwise there's another level, so...
            else
            {
                //activate the next level object
                nextLev.SetActive(true);

                //display the player score on the next level score object
                nextLevScore.GetComponent<Text>().text = "Your Score : " + playerScore;

                //update level to nect level
                MainManager.Instance.moveLevel();

                //update total player score
                MainManager.Instance.updateScore(playerScore);
            }
        }

        //stop the timer (to prevent game over if the player doesn't start the next level quickly enough
        Timer.GetComponent<Timer>().stopTimer();
    }

    //function to check the match made
    public void checkMatch(string matchName)
    {
        //add 20 for a match
        Scorer.GetComponent<Scorer>().updateScpre(20);

        //iterate though the targets list
        for (int i = 0; i < targetObjects.Count; i++)
        {
            //if the match name is the same as the name in the list...
            if (matchName == targetObjects[i].name)
            {
                //remove the target object from the list
                targetObjects.RemoveAt(i);

                //update the dctionary in the target diaplay
                targetsDisp.GetComponent<TargetDisp>().updateDict(matchName);

                //add additional 30 for a target match
                Scorer.GetComponent<Scorer>().updateScpre(30);
                
                //end the for loop (to prevent 1 match removing all required targets
                break;
            }
        }

        //if there's nothing left in the target list
        if (targetObjects.Count == 0)
        {
            //end the game with a win
            endGame(false, true);
        }
    }

    //function to create the target dictionary
    private void createTargetDict()
    {
        //iterate through the target list
        for (int i = 0; i < targetObjects.Count; i++)
        {
            //create a string of the target name
            string targetName = targetObjects[i].name;

            //if the target name is already a key in the dictionary...
            if (targetDict.ContainsKey(targetName))
            {
                //add 1 to the value for that key in the dictionary
                targetDict[targetName] = targetDict[targetName] + 1;
            } 
            //otherwise (not in dictionary)...
            else
            {
                //add the target name as key with a value of 1 to the dictionary
                targetDict.Add(targetName, 1);
            }
        }
    }

    //function to return to the start screen
    public void returnToStart()
    {
        //get the player score from the scorer element
        int playerScore = Scorer.GetComponent<Scorer>().getScore();

        //get the player inititlas from the player input
        playerInits = highScoreInput.GetComponent<HighScoreInput>().getUserInits();

        //add the score to the high score board within MainManger
        MainManager.Instance.addToHighScore(playerInits, playerScore);

        //load the start scene
        SceneManager.LoadScene("StartScene");
    }

    //function to load the game scene again (for next level)
    public void reloadScene()
    {
        //load the game scene
        SceneManager.LoadScene("GameScene");
    }
}
