using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : Spawn {

    //Variables
    public GameObject[] playerSpawns;    //List of possible player spawn points
    public GameObject currentSpawn;      //The player's current spawn point
    public bool isGenerated;             //Whether the level is randomly generated or not
    public GameObject[] players;         //The parent objects that contain the players;
    private TankData data;               //Spawned objects tank data
    [SerializeField]
    private int score;                   //The player's score
    [SerializeField]
    private int lives;                   //The player's lives
    [SerializeField]
    private bool firstSpawn = true;      //The first time the player spawns
    [SerializeField]
    private GameObject gameOverScreen;   //The player's game over screen
    [SerializeField]
    private Text gameOverText;           //The Game Over Text
    [SerializeField]
    private Text gameUIText;             //The Player's UI
    public bool isGameOver;              //If that player has a Game Over

    // Use this for initialization
    public override void Start () {
        //Run the base start
        base.Start();
        if (!isGenerated)
        {
            setSpawns();
        }

        //Find the player's parent objects
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        //If our gameObject isn't there
        if (spawnedObject == null)
        {
            if (!firstSpawn)
            {
                //Show Game Over Screen
                gameOverScreen.SetActive(true);
                gameUIText.enabled= false;
                //If they Have Lives Remaining
                if(lives > 0) { gameOverText.text = "Respawning in\n" + Mathf.Ceil(nextSpawnTime - Time.time) + " seconds."; }
                else
                {
                    //Else Show Game Over and Score
                    gameOverText.text = "G A M E   O V E R\nScore: " + score;
                    isGameOver = true;
                }
            }

            //And it is time to spawn
            if ((Time.time > nextSpawnTime && firstSpawn) || (Time.time > nextSpawnTime && lives > 0))
            {
                if (!firstSpawn) { gameOverScreen.SetActive(false); }

                //Pick a random spawn point
                currentSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)];
                
                //Spawn it and set the next time
                spawnedObject = Instantiate(prefab, currentSpawn.transform.position, currentSpawn.transform.rotation) as GameObject;
                if (players[0].transform.childCount == 0)
                {
                    //If theres no player 1 spawn, spawn for player 1
                    spawnedObject.transform.parent = players[0].transform;
                    if (firstSpawn)
                    {
                        //On first spawn, get everything for player 1
                        gameOverScreen = GameManager.instance.gameUI1.transform.GetChild(0).gameObject;
                        gameOverText = gameOverScreen.GetComponentInChildren<Text>();
                        gameUIText = GameManager.instance.gameUI1.transform.GetChild(1).gameObject.GetComponent<Text>();
                    }
                }
                else if(players[1].transform.childCount == 0)
                {
                    //If there is, spawn for player 2
                    spawnedObject.transform.parent = players[1].transform;
                    if (firstSpawn)
                    {
                        //On first spawn, get everything for player 2
                        gameOverScreen = GameManager.instance.gameUI2.transform.GetChild(0).gameObject;
                        gameOverText = gameOverScreen.GetComponentInChildren<Text>();
                        gameUIText = GameManager.instance.gameUI2.transform.GetChild(1).gameObject.GetComponent<Text>();
                    }
                }

                nextSpawnTime = Time.time + spawnDelay;

                data = spawnedObject.GetComponent<TankData>();

                if (firstSpawn)
                {
                    //Set Lives
                    lives = data.maxLives;
                }
                else
                {
                    //Lose a life on death
                    lives--;
                    //keep score
                    data.score = score;
                }

                firstSpawn = false;
            }
        }
        else
        {
            //Otherwise, the object still exists, so postpone the spawn
            gameOverScreen.SetActive(false);
            gameUIText.enabled = true;
            nextSpawnTime = Time.time + spawnDelay;
        }

        //Up date the UI
        score = data.score;
        gameUIText.text = "HP: " + data.hp + "\nLives: " + lives + "\nScore: " + score;
    }

    public void setSpawns()
    {
        playerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
    }

}
