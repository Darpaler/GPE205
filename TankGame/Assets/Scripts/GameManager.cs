using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Variables
    public static GameManager instance;     //Singleton
    public InputController player;          //Player
    public Camera main;                     //Main Camera
    public FollowGameObject cameraFollow;   //Camera's Follow Game Object Component
    public List<TankData> enemies;          //List of enemies
    public List<PickUp> pickUps;            //List of pick ups
    public List<PickUpWeapon> weapons;      //List of pick up weaopons
    public GameObject mainMenu;             //The main menu
    public GameObject pauseMenu;            //The pause menu
    public GameObject level;                //The level prefab
    public GameObject currentLevel;         //The current running level
    public Text highScoreText;              //The high score display
    private int currentHighScore;           //The current high score



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

        Load();

        RunMainMenu();

    }
	
	// Update is called once per frame
	void Update () {
        if(cameraFollow.targetObjectTransform == null)
        {
            //Set Camera
            cameraFollow.targetObjectTransform = player.gameObject.GetComponent<Transform>();
        }
		
	}

    public void RunMainMenu()
    {
        main.gameObject.transform.parent = instance.transform;
        Destroy(currentLevel);
        pauseMenu.SetActive(false);
        Save();
        mainMenu.SetActive(true);
    }
    public void RunPauseMenu()
    {
        main.gameObject.transform.parent = instance.transform;
        currentLevel.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void RunLevel()
    {
        if (currentLevel == null)
        {
            currentLevel = Instantiate(level);
        }
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        currentLevel.SetActive(true);
    }

    public void Save()
    {
        if(player.data.score > currentHighScore)
        {
            currentHighScore = player.data.score;
            PlayerPrefs.SetInt("highScore", currentHighScore);
            highScoreText.text = "High Score: " + currentHighScore;
            PlayerPrefs.SetString("highScoreString", highScoreText.text);
            PlayerPrefs.Save();
        }
    }
    public void Load()
    {
        currentHighScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = PlayerPrefs.GetString("highScoreString");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Savign and Quitting");
        Save();
    }

}
