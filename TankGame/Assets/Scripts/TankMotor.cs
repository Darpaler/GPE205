using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TankMotor : MonoBehaviour {

    //Variables
    private CharacterController characterController;    //Character Controller Component
    private Transform tf;                               //Transform Component

    // Use this for initialization
    void Start ()
    {
        //Set Components
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Movement Functions
    public void Move(float speed)       //Move Foward/Back
    {
        characterController.SimpleMove(tf.forward * speed);    //Move by how fast you are
    }
    
    public void Rotate(float speed)     //Turn Left/Right
    {
        tf.Rotate(Vector3.up * speed * Time.deltaTime);        //Turn by how fast you are per second
    }

    public void Shoot(GameObject shell)                 //Shoot a Shell
    {
        Instantiate(shell);
    }

}
