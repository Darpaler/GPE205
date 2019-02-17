using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour {

    //Variables
    public float moveSpeed = 3;                                    //Move Speed in meters per second
    public float reverseSpeed = 1.5f;                              //Reverse Speed in meters per second
    public float turnSpeed = 180;                                  //Turn Speed in degrees per second
    public GameObject shell;                                       //Current Weapon
    public Vector2 shootOffset = new Vector2(0.8f, 0.25f);         //Offset From Center (Horizontal, Vertical)
    public int hp;                                                 //Current Hit Points
    public int pointValue;                                         //How Many Points This Tank's Worth
    public int score;                                              //How Many Points This Tank Has
}
