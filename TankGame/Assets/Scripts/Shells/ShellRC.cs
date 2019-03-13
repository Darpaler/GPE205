using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRC : Shell {

    //Variables
    private TankMotor playerMotor;
    private TankData playerData;
    private Transform tf;
    private FollowGameObject camera;
    public Vector3 offset;

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
        camera = GameManager.instance.main.GetComponent<FollowGameObject>();

        //Set camera
        camera.offset = offset;
        camera.targetObjectTransform = tf;

    }

    void Update()
    {
        tf.position += transform.forward * data.speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Get The Data Of Who We Hit
        TankData hitData = collision.gameObject.GetComponent<TankData>();

        //If They Have Data, They Are A Tank
        if (hitData != null)
        {
            //Set Their health And Play Their Hit Function
            hitData.hp = collision.gameObject.GetComponent<TankMotor>().Hit(hitData.hp, data.damage);

            //If They Died
            if (hitData.hp - data.damage <= 0)
            {
                //Whoever Shot The Shell Gets Points For Who They Killed
                transform.parent.GetComponentInChildren<TankData>().score += hitData.pointValue;
            }
        }

        //Destroy Shell After Collision
        GameManager.instance.main.transform.parent = GameManager.instance.gameObject.GetComponent<Transform>();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        camera.offset = camera.baseOffset;
        playerMotor.nextShotTime = Time.time + gameObject.GetComponent<ShellData>().fireRate;
        GameManager.instance.player.motor = playerMotor;
        GameManager.instance.player.data = playerData;
    }
}
