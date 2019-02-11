using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]

public class SampleAIController : MonoBehaviour
{

    //Variables
    private Transform tf;                            //Transform component
    private TankMotor motor;                         //TankMotor component
    private TankData data;                           //TankData component
    public Transform[] waypoints;                    //The waypoints the AI will follow
    private int currentWaypoint = 0;                 //The waypoint we're heading towards
    public float closeEnough = 1.0f;                 //How close the tank has to get to the waypoint
    public enum LoopType { Stop, Loop, PingPong };   //List of different waypoint loop types
    public LoopType loopType;                        //The loop type we're using
    private bool isPatrolForward = true;             //If we're patrolling forward or not for PingPong loop type

    // Use this for initialization
    void Awake()
    {

        //Get Components
        tf = gameObject.GetComponent<Transform>();
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!(motor.RotateTowards(waypoints[currentWaypoint].position, data.turnSpeed)))
        {
            //Move forward
            motor.Move(data.moveSpeed);
        }

        // If we are close to the waypoint,
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            //Switch Different Input Types
            switch (loopType)
            {
                case LoopType.Stop:

                    // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    break;

                case LoopType.Loop:

                    // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        currentWaypoint = 0;
                    }
                    break;

                case LoopType.PingPong:

                    if (isPatrolForward)
                    {
                        // Advance to the next waypoint, if we are still in range
                        if (currentWaypoint < waypoints.Length - 1)
                        {
                            currentWaypoint++;
                        }
                        else
                        {
                            //Otherwise reverse direction and decrement our current waypoint
                            isPatrolForward = false;
                            currentWaypoint--;
                        }
                    }
                    else
                    {
                        // Advance to the next waypoint, if we are still in range
                        if (currentWaypoint > 0)
                        {
                            currentWaypoint--;
                        }
                        else
                        {
                            //Otherwise reverse direction and increment our current waypoint
                            isPatrolForward = true;
                            currentWaypoint++;
                        }
                    }
                    break;
            }
        }
    }
}
