using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Variables
    public static GameManager instance;     //Singleton
    public InputController player;          //Player
    public Camera main;                     //Main Camera
    public FollowGameObject cameraFollow;   //Camera's Follow Game Object Component
    public List<TankData> enemies;          //List of enemies
    public List<PickUp> pickUps;            //List of pick ups
    public List<PickUpWeapon> weapons;      //List of pick up weaopons


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

	}
	
	// Update is called once per frame
	void Update () {

        if(cameraFollow.targetObjectTransform == null)
        {
            //Set Camera
            cameraFollow.targetObjectTransform = player.gameObject.GetComponent<Transform>();
        }
		
	}
}
