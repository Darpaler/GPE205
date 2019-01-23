using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {
    public TankMotor motor;
    public TankData data;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        motor.Move(data.moveSpeed);
        motor.Rotate(data.turnSpeed);
    }
}