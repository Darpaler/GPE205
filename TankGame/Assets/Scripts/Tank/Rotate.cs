using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    //Variables
    private Transform tf;       //Transform Component
    public float rotateSpeed;   //How fast we want to rotate

	// Use this for initialization
	void Start () {

        //Get Components
        tf = gameObject.GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        //Rotate our object
        tf.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);

	}
}
