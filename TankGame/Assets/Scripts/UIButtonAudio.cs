using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonAudio : MonoBehaviour {

    //Variables
    private Button button;
    private Transform tf;
    public AudioClip up;
    public AudioClip down;


	// Use this for initialization
	void Start () {

        //Get Components
        button = gameObject.GetComponent<Button>();
        tf = GetComponent<Transform>();
        button.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        AudioSource.PlayClipAtPoint(down, tf.position, GameManager.instance.sfxVolume);
    }
}
