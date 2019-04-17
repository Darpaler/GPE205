using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUp
{
    //Variables
    public float speedModifier;         //Modifies player's speed
    public float healthModifier;        //Modifies player's health
    public float maxHealthModifier;     //Modifies player's max health
    public float fireRateModifier;      //Modifies shell's fireRate
    public float duration;              //Power ups durations
    public bool isPermanent;            //Wether the power up is permanent or not

    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        healthModifier = Mathf.Clamp(healthModifier, 0, target.maxHp - target.hp);
        target.hp += healthModifier;
        target.maxHp += maxHealthModifier;
        target.fireRateModifier += fireRateModifier;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.hp -= healthModifier;
        target.maxHp -= maxHealthModifier;
        target.fireRateModifier -= fireRateModifier;
    }

}
