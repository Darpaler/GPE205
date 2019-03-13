using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour {

    //Variables
    public Transform targetObjectTransform; //Target Object
    public Vector3 offset;                  //Offset
    public Vector3 baseOffset;       //Default Offset
    private Transform tf;                   //Transform Component

	// Use this for initialization
	void Start ()
	{
        //Get Components
	    tf = GetComponent<Transform>();

        //Save Offset
        baseOffset = offset;

	}
	
	// Update is called once per frame
	void Update ()
	{
        if(tf.parent != targetObjectTransform)
        {
            tf.SetParent(targetObjectTransform);
            tf.localPosition = offset;
            tf.localRotation = Quaternion.identity;
        }
	}
}
