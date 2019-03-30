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
    public List<ScoreData> scores;          //The high score list
    public Text highScoreText;              //The high score display



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
        Save();
        main.gameObject.transform.parent = instance.transform;
        Destroy(currentLevel);
        pauseMenu.SetActive(false);
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
        scores.Add(new ScoreData(player.data.name, player.data.score));
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
