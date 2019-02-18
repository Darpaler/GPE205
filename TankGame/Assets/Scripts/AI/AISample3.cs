using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankData))]

public class AISample3 : MonoBehaviour {

    //Variables
    public Transform target;                //Who to chase
    private TankMotor motor;                //TankMotor Component
    private TankData data;                  //TankData Component
    private Transform tf;                   //Transform Component
    private int avoidanceStage = 0;         //Obstacle avoidance stage
    public float avoidanceTime = 2.0f;      //How long the AI would avoid and obstacle before returning going back to the target
    private float exitTime;                 //How long until we exit the avoidance stage
    public enum AttackMode { Chase };       //The attack mode for the AI
    public AttackMode attackMode;           //The current attack mode

    // Use this for initialization
    void Start () {

        //Get Components
        motor = GetComponent<TankMotor>();
        data = GetComponent<TankData>();
        tf = GetComponent<Transform>();

        //Set Target
        target = GameManager.instance.player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoChase();
            }
        }
    }

    void DoChase()
    {
        //Face target
        motor.RotateTowards(target.position, data.turnSpeed);
        //Check if we can move
        if (CanMove(data.moveSpeed))
        {
            motor.Move(data.moveSpeed);
        }
        else
        {
            //Enter obstacle avoidance stage 1
            avoidanceStage = 1;
        }
    }

    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            //Rotate left
            motor.Rotate(-1 * data.turnSpeed);

            //If I can now move forward, move to stage 2
            if (CanMove(data.moveSpeed))
            {
                avoidanceStage = 2;

                //Set the number of seconds we will stay in stage 2
                exitTime = avoidanceTime;
            }

            //If not, try again
        }
        else if (avoidanceStage == 2)
        {
            //If we can move forward, do so
            if (CanMove(data.moveSpeed))
            {
                //Subtract from our timer and move
                exitTime -= Time.deltaTime;
                motor.Move(data.moveSpeed);

                //If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
            else
            {
                //If not, repeat avoidance stages
                avoidanceStage = 1;
            }
        }
    }

    bool CanMove(float speed)
    {
        //If our raycast hit something
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            //If it isn't the player
            if (!hit.collider.CompareTag("Player"))
            {
                //There's an obstacle
                return false;
            }
        }
        //If not, we can move
        return true;
    }
}
