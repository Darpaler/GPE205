using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRC : Shell {

    //Variables
    private TankMotor playerMotor;
    private TankData playerData;
    private Transform tf;

    // Use this for initialization
    public override void Start()
    {
        //Get Components
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<ShellData>();
        playerMotor = GameManager.instance.player.motor;
        playerData = GameManager.instance.player.data;
        GameManager.instance.player.motor = gameObject.GetComponent<TankMotor>();
        GameManager.instance.player.data = gameObject.GetComponent<TankData>();

    }

    void Update()
    {
        tf.position += transform.forward * data.speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        playerMotor.nextShotTime = Time.time + gameObject.GetComponent<ShellData>().fireRate;
        GameManager.instance.player.motor = playerMotor;
        GameManager.instance.player.data = playerData;
    }
}
