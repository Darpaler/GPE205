using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Variables
    public static GameManager instance;             //Singleton
    public InputController player;                  //Player 1
    public InputController player2;                 //Player 2
    public Camera main;                             //Main Camera
    public Camera camera2;                          //Player 2's Camera
    public FollowGameObject cameraFollow;           //Camera's Follow Game Object Component
    public FollowGameObject camera2Follow;          //Player 2's Camera's Follow Game Object Component
    public List<TankData> enemies;                  //List of enemies
    public List<PickUp> pickUps;                    //List of pick ups
    public List<PickUpWeapon> weapons;              //List of pick up weaopons
    public GameObject mainMenu;                     //The main menu
    public GameObject pauseMenu;                    //The pause menu
    public GameObject level;                        //The level prefab
    public GameObject currentLevel;                 //The current running level
    public List<ScoreData> scores;                  //The high score list
    public Text highScoreText;                      //The high score display
    public GameObject gameUI1;                      //Player 1's UI screen
    public GameObject gameUI2;                      //Player 2's UI screen
    public LevelGenerator currentLevelGenerator;    //The current level's Level Generator Component
    public GameObject restart;                      //The restart button
    public float sfxVolume = 1.0f;                  //The SFX Volume
    private int p1Score;                            //Player 1's end score
    private string p1Name;                          //Player 1's end name
    private int p2Score;                            //Player 2's end score
    private string p2Name;                          //Player 2's end name


    // Runs before any Start() functions run
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameManager.");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        //Get Components
        cameraFollow = main.GetComponent<FollowGameObject>();
        camera2Follow = camera2.GetComponent<FollowGameObject>();

        //Load Save Data
        Load();

        //Run the Main Menu
        RunMainMenu();

    }
	
	// Update is called once per frame
	void Update () {
        if(cameraFollow.targetObjectTransform == null && player != null)
        {
            //Set Player 1 Camera
            cameraFollow.targetObjectTransform = player.gameObject.GetComponent<Transform>();
        }
        if(camera2Follow.targetObjectTransform == null && player2 != null)
        {
            //Set Player 2 Camera
            camera2Follow.targetObjectTransform = player2.gameObject.GetComponent<Transform>();
        }
        if(currentLevelGenerator != null)
        {
            if (currentLevelGenerator.isGameOver)
            {
                //When both players are dead, let them restart
                restart.SetActive(true);
            }
        }

        //Update current Scores
        if(player != null)
        {
            p1Score = player.data.score;
            p1Name = player.data.name;
        }
        if (player != null)
        {
            p2Score = player2.data.score;
            p2Name = player2.data.name;
        }

    }

    public void RunMainMenu()
    {
        //Save
        Save();
        //Take cameras off players
        main.gameObject.transform.parent = instance.transform;
        camera2.gameObject.transform.parent = instance.transform;
        //Destroy the level
        Destroy(currentLevel);
        //Hide other screens
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void RunPauseMenu()
    {
        //Take cameras off players
        main.gameObject.transform.parent = instance.transform;
        camera2.gameObject.transform.parent = instance.transform;
        //Pause the game
        currentLevel.SetActive(false);
        //Hide other screens
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void RunLevel()
    {
        //If there isn't already a level
        if (currentLevel == null)
        {
            //Make one
            currentLevel = Instantiate(level);
            currentLevelGenerator = currentLevel.GetComponent<LevelGenerator>();
            restart.SetActive(false);
        }
        //Else just hide other screens
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        currentLevel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Save()
    {
        //Add both player's scores
        scores.Add(new ScoreData(p1Name, p1Score));
        scores.Add(new ScoreData(p2Name, p2Score));
        //Sort the list of scores
        scores.Sort();
        //Show from highest to least
        scores.Reverse();
        //Only show the top three
        scores = scores.GetRange(0, 3);
        
        //Display them
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString("name" + i, scores[i].name);
            PlayerPrefs.SetFloat("score" + i, scores[i].score);
        }
        highScoreText.text = "HIGHSCORES\n";
        for (int i = 0; i < scores.Count; i++)
        {
            highScoreText.text += scores[i].name + ": " + scores[i].score + "\n";
        }
        //Save the high scores
        PlayerPrefs.SetString("highScoreText", highScoreText.text);
        PlayerPrefs.Save();

    }
    public void Load()
    {
        //Get the list of scores
        for (int i = 0; i < 3; i++)
        {
            scores.Add(new ScoreData(PlayerPrefs.GetString("name" + i), PlayerPrefs.GetFloat("score" + i)));
        }
        //Get the display
        highScoreText.text = PlayerPrefs.GetString("highScoreText");
    }

    private void OnApplicationQuit()
    {
        //Save on quit
        Debug.Log("Saving and Quitting");
        Save();
    }
}
