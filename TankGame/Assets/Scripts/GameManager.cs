using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Variables
    public static GameManager instance;     //Singleton
    public InputController player;          //Player
    public InputController player2;
    public Camera main;                     //Main Camera
    public Camera camera2;
    public FollowGameObject cameraFollow;   //Camera's Follow Game Object Component
    public FollowGameObject camera2Follow;
    public List<TankData> enemies;          //List of enemies
    public List<PickUp> pickUps;            //List of pick ups
    public List<PickUpWeapon> weapons;      //List of pick up weaopons
    public GameObject mainMenu;             //The main menu
    public GameObject pauseMenu;            //The pause menu
    public GameObject level;                //The level prefab
    public GameObject currentLevel;         //The current running level
    public List<ScoreData> scores;          //The high score list
    public Text highScoreText;              //The high score display
    public GameObject gameUI1;              //Player 1's UI screen
    public GameObject gameUI2;              //Player 2's UI screen
    public LevelGenerator currentLevelGenerator;
    public GameObject restart;
    public float sfxVolume = 1.0f;



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

        Load();

        RunMainMenu();

    }
	
	// Update is called once per frame
	void Update () {
        if(cameraFollow.targetObjectTransform == null && player != null)
        {
            //Set Camera
            cameraFollow.targetObjectTransform = player.gameObject.GetComponent<Transform>();
        }
        if(camera2Follow.targetObjectTransform == null && player2 != null)
        {
            camera2Follow.targetObjectTransform = player2.gameObject.GetComponent<Transform>();
        }
        if (currentLevelGenerator.isGameOver)
        {
            restart.SetActive(true);            
        }
		
	}

    public void RunMainMenu()
    {
        Save();
        main.gameObject.transform.parent = instance.transform;
        camera2.gameObject.transform.parent = instance.transform;
        Destroy(currentLevel);
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void RunPauseMenu()
    {
        main.gameObject.transform.parent = instance.transform;
        camera2.gameObject.transform.parent = instance.transform;
        currentLevel.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void RunLevel()
    {
        if (currentLevel == null)
        {
            currentLevel = Instantiate(level);
            currentLevelGenerator = currentLevel.GetComponent<LevelGenerator>();
            restart.SetActive(false);
        }
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
        scores.Add(new ScoreData(player.data.name, player.data.score));
        scores.Add(new ScoreData(player2.data.name, player2.data.score));
        scores.Sort();
        scores.Reverse();
        scores = scores.GetRange(0, 3);
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
        PlayerPrefs.SetString("highScoreText", highScoreText.text);
        PlayerPrefs.Save();

    }
    public void Load()
    {
        for (int i = 0; i < 3; i++)
        {
            scores.Add(new ScoreData(PlayerPrefs.GetString("name" + i), PlayerPrefs.GetFloat("score" + i)));
        }
        highScoreText.text = PlayerPrefs.GetString("highScoreText");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Saving and Quitting");
        Save();
    }

}
