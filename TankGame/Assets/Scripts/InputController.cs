using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]

public class InputController: MonoBehaviour
{
    //Variables
    public enum InputScheme { WASD, arrowKeys };    //Input Types
    public InputScheme input = InputScheme.WASD;    //Current Input
    private TankData data;                          //TankData Component
    private TankMotor motor;                        //TankMotor Component

    // Use this for initialization
    void Start ()
    {
        //Get Components
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Switch Different Input Types
        switch (input)
        {
            //Arrow Keys
            case InputScheme.arrowKeys:
                                                        //Up
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveSpeed);
                }
                                                        //Down
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.reverseSpeed);
                }
                                                        //Right
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.turnSpeed);
                }
                                                        //Left
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.turnSpeed);
                }
                                                        //Shoot
                if (Input.GetKey(KeyCode.Keypad0))
                {
                    motor.Shoot(data.shell, data.shootOffset);
                }
                break;

            //WASD
            case InputScheme.WASD:
                                                        //Up 
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveSpeed);
                }
                                                        //Down
                if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.moveSpeed);
                }
                                                        //Right
                if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.turnSpeed);
                }
                                                        //Left
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.turnSpeed);
                }
                                                        //Shoot
                if (Input.GetKey(KeyCode.Space))
                {
                    motor.Shoot(data.shell, data.shootOffset);
                }
                break;
        }
    }
}
