using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]

public class SampeAI2 : MonoBehaviour {

    //Variables

    //Components
    private Transform tf;                             //Transform Component
    private TankData data;                            //TankData Component
    private TankMotor motor;                          //TankMotor Component
    public enum AttackMode { Chase, Flee, Stop };     //The tanks attack mode

    [Header("State")]
    public AttackMode attackMode;                     //The tanks current attack mode

    [Header("Senses")]
    public Transform target;                          //Who to chase
    public float visionDistance;                      //How far the tank can see
    public float visionAngle;                         //Angle of the tanks field of view
    public float hearingRadius;                       //How far the tank can hear
    public float fleeDistance = 1.0f;                 //How far to flee

    [Header("Debug")]
    public bool showDebug;                            //Weather or not we can see debug lines
    const float DEBUG_ANGLE_DISTANCE = 2.0f;
    const float DEGREES_TO_RADIANS = Mathf.PI / 180.0f;

    // Use this for initialization
    void Start () {

        //Get Components
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        target = GameManager.instance.player.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        if (CanHear(target.gameObject))
        {
            motor.RotateTowards(target.position, data.turnSpeed);
        }

        if (CanSee(target.gameObject))
        {
            attackMode = AttackMode.Chase;
        }
        else
        {
            attackMode = AttackMode.Stop;
        }


        switch (attackMode)
        {
            case AttackMode.Chase:
                // Rotate towards the target
                motor.RotateTowards(target.position, data.turnSpeed);
                // Move forward
                motor.Move(data.moveSpeed);
                break;

            case AttackMode.Flee:
                
                //Variables
                Vector3 vectorAwayFromTarget = -1 * (target.position - tf.position);    //The vector away from our target
                vectorAwayFromTarget.Normalize();                                       //Normalized vector

                //Rotate away from target
                motor.RotateTowards(vectorAwayFromTarget * fleeDistance + tf.position, data.turnSpeed);
                //Move there
                motor.Move(data.moveSpeed);
                break;
        }
    }

    public bool CanHear(GameObject target)
    {
        //Check for a noise maker
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        if (targetNoiseMaker == null)
        {
            return false;
        }

        //Check if we can hear them
        Transform targetTf = target.GetComponent<Transform>();
        if (Vector3.Distance(targetTf.position, tf.position) <= targetNoiseMaker.volume + hearingRadius)
        {
            return true;
        }

        //If we can't, return false
        return false;
    }

    public bool CanSee(GameObject target)
    {
        //Check for a collider
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider == null)
        {
            return false;
        }

        //Find vector to target
        Vector3 vectorToTarget = target.GetComponent<Transform>().position - tf.position;
        vectorToTarget.Normalize();

        //Debug
        DrawDebugAngle(showDebug);

        //If they're outside our field of view, we can't see them
        if (Vector3.Angle(vectorToTarget, tf.forward) >= visionAngle)
        {
            return false;
        }

        // Make sure nothing is blocking our view
        Ray ray = new Ray(tf.position, vectorToTarget);
        RaycastHit hitInfo;

        //If we hit nothing, return false
        if (!Physics.Raycast(ray, out hitInfo, visionDistance))
        {
            return false;
        }

        if (showDebug)
        {
            Debug.DrawLine(tf.position, tf.position + vectorToTarget * visionDistance, Color.red);
            Debug.Log("Hit: " + hitInfo.collider + "\nTarget: " + targetCollider);
        }

        // if our raycast hits nothing, we can't see them
        if (!Physics.Raycast(tf.position, vectorToTarget, visionDistance))
        {
            return false;
        }

        // If our raycast hits them first, then we can see them
        if (hitInfo.collider == targetCollider)
        {
            return true;
        }

        // otherwise, we hit something else
        return false;
    }

    public void DrawDebugAngle(bool show)
    {
        if (show)
        {
            Vector3 perpendicularDirection = new Vector3(-tf.forward.y, tf.forward.x);
            float oppositeSideLength = Mathf.Tan(visionAngle * 0.5f * DEGREES_TO_RADIANS) * DEBUG_ANGLE_DISTANCE;
            Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.forward + perpendicularDirection * oppositeSideLength, Color.green);
            Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.forward - perpendicularDirection * oppositeSideLength, Color.green);
        }
    }

}
