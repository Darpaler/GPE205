using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour {
    
    //Variables
    private Transform tf;       //Transform Component
    public Shell shell;         //The shell to give the player
    public AudioClip feedback;  //Audio feedback

    // Use this for initialization
    void Start()
    {
        //Get Components
        tf = GetComponent<Transform>();

        //Set GameManager
        GameManager.instance.weapons.Add(this);

    }

    public void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's tank data - if it has one
        TankData data = other.GetComponent<TankData>();

        //If the other object has a PowerUpController
        if (data != null)
        {
            //Add the shell
            data.shell = shell.gameObject;

            //Play Feedback (if it is set)
            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }
            //Destroy this powerup
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameManager.instance.weapons.Add(this);
    }
}
