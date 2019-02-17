using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Variables
    public static GameManager instance;     //Singleton
    public InputController player;          //Player


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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
