using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(ShellData))]

public class Shell : MonoBehaviour {

    private Rigidbody rb;       //Rigidbody Component
    private ShellData data;     //ShellData Component


	// Use this for initialization
	void Start ()
    {
        //Get Components
        rb = gameObject.GetComponent<Rigidbody>();
        data = gameObject.GetComponent<ShellData>();

        //Fire Shell
        rb.AddForce(transform.forward * data.speed, ForceMode.Impulse);

        //Destroy Shell After Time
        Destroy(gameObject, data.timeTillDestroy);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //Get The Data Of Who We Hit
        TankData hitData = collision.gameObject.GetComponent<TankData>();

        //If They Have Data, They Are A Tank
        if (hitData != null)
        {
            //Set Their health And Play Their Hit Function
            hitData.hp = collision.gameObject.GetComponent<TankMotor>().Hit(hitData.hp, data.damage);

            //If They Died
            if (hitData.hp == 0)
            {
                //Whoever Shot The Shell Gets Points For Who They Killed
                transform.parent.GetComponentInChildren<TankData>().score += hitData.pointValue;
            }
        }

        //Destroy Shell After Collision
        Destroy(gameObject);

    }
}
