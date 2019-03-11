using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    //Variables
    private Transform tf;       //Transform Component
    public PowerUp powerUp;     //The power up the player get's on pick up
    public AudioClip feedback;  //Audio feedback

    // Use this for initialization
    void Start () {

        //Get Components
        tf = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's PowerupController - if it has one
        PowerUpController powCon = other.GetComponent<PowerUpController>();

        //If the other object has a PowerUpController
        if (powCon != null)
        {
            //Add the powerUp
            powCon.Add(powerUp);

            //Play Feedback (if it is set)
            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }
            //Destroy this powerup
            Destroy(gameObject);
        }
    }
}
