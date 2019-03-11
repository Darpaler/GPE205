using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : Spawn {

    //Variables
    public GameObject[] playerSpawns;    //List of possible player spawn points
    public GameObject currentSpawn;      //The player's current spawn point
    public bool isGenerated;             //Whether the level is randomly generated or not

	// Use this for initialization
	public override void Start () {
        //Run the base start
        base.Start();
        if (!isGenerated)
        {
            setSpawns();
        }
        spawnedObject = GameManager.instance.player.gameObject;


	}

    void Update()
    {
        //If our gameObject isn't there
        if (spawnedObject == null)
        {
            //And it is time to spawn
            if (Time.time > nextSpawnTime)
            {
                //Pick a random spawn point
                currentSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)];

                //Spawn it and set the next time
                spawnedObject = Instantiate(prefab, currentSpawn.transform.position, currentSpawn.transform.rotation, GameObject.FindGameObjectWithTag("Player").transform) as GameObject;
                GameManager.instance.player = spawnedObject.GetComponent<InputController>();
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            //Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    public void setSpawns()
    {
        playerSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");
    }

}
