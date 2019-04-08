using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonAudio : MonoBehaviour {

    //Variables
    private Button button;      //The button
    private Transform tf;       //The transform component
    public AudioClip down;      //The audio clip


	// Use this for initialization
	void Start () {

        //Get Components
        button = gameObject.GetComponent<Button>();
        tf = GetComponent<Transform>();
        button.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        //Play audio
        AudioSource.PlayClipAtPoint(down, tf.position, GameManager.instance.sfxVolume);
    }
}
