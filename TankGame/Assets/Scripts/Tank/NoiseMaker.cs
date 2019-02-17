using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour {

    //Variables
    public float volume;                //How far away the tank can be heard by AI
    public float decayPerFrameDraw;     //How much the volume decreases per frame

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Decay Volume
        if(volume > 0)
        {
            volume -= decayPerFrameDraw;
        }
		
	}
}
