using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]

public class SampeAI2 : MonoBehaviour {

    //Variables
    public enum AttackMode { Chase, Flee };     //The tanks attack mode
    public AttackMode attackMode;               //The tanks current attack mode
    public Transform target;                    //Who to chase
    private TankData data;                      //TankData Component
    private TankMotor motor;                    //TankMotor Component

    // Use this for initialization
    void Start () {

        //Get Components
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
