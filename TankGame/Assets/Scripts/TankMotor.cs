using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TankMotor : MonoBehaviour {

    //Variables
    private CharacterController characterController;    //Character Controller Component
    private Transform tf;                               //Transform Component
    private float nextShotTime;                         //Reload Time

    // Use this for initialization
    void Start ()
    {
        //Set Components
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        nextShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Movement Functions
    public void Move(float speed)                              //Move Foward/Back
    {
        characterController.SimpleMove(tf.forward * speed);    //Move By How Fast You Are
    }
    
    public void Rotate(float speed)                            //Turn Left/Right
    {
        tf.Rotate(Vector3.up * speed * Time.deltaTime);        //Turn By How Fast You Are Per Second
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        //Variables
        Vector3 vectorToTarget;     //The vector to where our target is

        //Find the difference between us and out target
        vectorToTarget = target - tf.position;

        // Find the Quaternion that looks down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        // If that is the direction we are already looking, we don't need to turn!
        if (targetRotation == tf.rotation)
        {
            return false;
        }

        //Else, rotate
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);

        // We rotated, so return true
        return true;
    }

    public void Shoot(GameObject shell, Vector3 offset)        //Shoot A Shell
    {
        if (Time.time >= nextShotTime)                         //If You Waited The Reload Time
        {
            Instantiate(shell, tf.position + (tf.forward * offset.x) + (tf.up * offset.y), tf.rotation, tf.parent);        //Create A Shell
            nextShotTime = Time.time + shell.GetComponent<ShellData>().fireRate;                                           //Reset Reload Time
        }
    }

    //Damage Function
    public int Hit(int hp, int damage)
    {
        hp -= damage;               //Deal Damage   
        if (hp <= 0)                //If HP Hits 0
        {
            Destroy(gameObject);
            return 0;
        }
        else                        //If Not
        {
            return hp;              //Return HP
        }
    }
}
