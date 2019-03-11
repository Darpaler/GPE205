using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    //Variables
    public GameObject prefab;           //Prefab to spawn
    public float spawnDelay;            //Time in between spawns
    private float nextSpawnTime;        //The time until the next spawn
    private Transform tf;               //Transform Component
    private GameObject spawnedObject;   //The object we spawned 

    // Use this for initialization
    void Start () {

        //Get Components
        tf = GetComponent<Transform>();
        nextSpawnTime = Time.time;

    }

    void Update()
    {
        //If our gameObject isn't there
        if (spawnedObject == null)
        {
            //And it is time to spawn
            if (Time.time > nextSpawnTime)
            {
                //Spawn it and set the next time
                spawnedObject = Instantiate(prefab, tf.position, prefab.transform.rotation) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            //Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
