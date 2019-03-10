using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour {

    //Variables
    public PowerUpController powCon;        //Power Up Controller Component
    public PowerUp cheatPowerUp;            //Power Up Component

    // Use this for initialization
    void Start()
    {
        if (powCon == null)
        {
            powCon = gameObject.GetComponent<PowerUpController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If we press D then P, activate our cheat
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Cheat Added.");
            // Add our powerup to the tank
            powCon.Add(cheatPowerUp);
        }
    }
}
