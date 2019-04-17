using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour {
    
    //Variables
    private Transform tf;       //The transform component
    public float speed;         //The speed that the shell moves horizontally
    public float sinLength;

	// Use this for initialization
	void Start () {

        //Get Components
        tf = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
        tf.position += transform.right * sinLength * (Mathf.Sin(Time.time * speed)/100);
	}
}
