using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]

public class AIController : MonoBehaviour
{
    //Variables
    private TankData data;                          //TankData Component
    private TankMotor motor;                        //TankMotor Component

    // Use this for initialization
    void Start()
    {
        //Get Components
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.Shoot(data.shell, data.shootOffset);      //Shoot
    }
}
