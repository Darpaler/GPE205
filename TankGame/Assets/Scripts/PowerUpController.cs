using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    //Variables
    private TankData data;              //Tank Data Component
    public List<PowerUp> powerUps;      //The power ups currently on our tank

    // Use this for initialization
    void Start()
    {

        //Get Components
        data = GetComponent<TankData>();

        //Initialize Lists
        powerUps = new List<PowerUp>();

    }

    // Update is called once per frame
    void Update()
    {
        // Create an List to hold our expired powerups
        List<PowerUp> expiredPowerUps = new List<PowerUp>();

        // Loop through all the powers in the List
        foreach (PowerUp power in powerUps)
        {
            // Subtract from the timer
            power.duration -= Time.deltaTime;

            // Assemble a list of expired powerups
            if (power.duration <= 0)
            {
                expiredPowerUps.Add(power);
            }
        }
        // Now that we've looked at every powerup in our list, use our list of expired powerups to remove the expired ones.
        foreach (PowerUp power in expiredPowerUps)
        {
            power.OnDeactivate(data);
            powerUps.Remove(power);
        }
    }

    public void Add(PowerUp powerUp)
    {
        // Run the OnActivate function of the powerup
        powerUp.OnActivate(data);

        // Only add the non permanent ones to the list
        if (!powerUp.isPermanent)
        {
            powerUps.Add(powerUp);
        }
    }
}
