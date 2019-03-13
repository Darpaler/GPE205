using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRCMotor : TankMotor {

    public override void Start()
    {
        base.Start();
        nextShotTime = Time.time + gameObject.GetComponent<ShellData>().fireRate;
    }

    public override void Move(float speed)
    {
        //Do Nothing
    }

    public override void Shoot(GameObject shell, Vector3 offset, float fireRateModifier)
    {
        if (Time.time >= nextShotTime)                         //If You Waited The Reload Time
        {
            Debug.Log(GameManager.instance.player.gameObject);
            GameManager.instance.main.GetComponent<FollowGameObject>().targetObjectTransform = GameManager.instance.player.transform;
            GameManager.instance.main.GetComponent<FollowGameObject>().transform.SetParent(GameManager.instance.transform);
            Debug.Log(GameManager.instance.main.transform.parent);
            Destroy(gameObject);
        }
    }
}
