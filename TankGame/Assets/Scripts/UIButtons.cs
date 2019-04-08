using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour {

    //Variables
    public Dropdown playerSelect;       //Selects either multiplayer or singleplayer
    public Dropdown mapType;            //Selects the map type
    public InputField mapSeed;          //Type in custom map seed
    public Slider volume;               //Volume Slider
    public Slider sfxVolume;            //The SFX Slider
    private AudioSource bgm;            //The BGM source
    private LevelGenerator level;       //The level

	// Use this for initialization
	void Start () {
        //Get Components
        level = GameManager.instance.level.GetComponent<LevelGenerator>();
        bgm = GameManager.instance.gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
        switch (playerSelect.value)
        {
            case 0:
                //Set Multiplayer
                level.multiplayer = false;
                //Set Screens
                GameManager.instance.gameUI1.GetComponent<RectTransform>().offsetMin = new Vector2(0, -360);
                GameManager.instance.gameUI2.SetActive(false);
                break;
            case 1:
                //Set Multiplayer
                level.multiplayer = true;
                //Set Screens
                GameManager.instance.gameUI1.GetComponent<RectTransform>().offsetMin = new Vector2(0, -180);
                GameManager.instance.gameUI2.SetActive(true);
                break;
        }
        switch (mapType.value)
        {
            case 0:
                //Set Map Type
                level.mapType = LevelGenerator.MapType.mapOfTheDay;
                mapSeed.gameObject.SetActive(false);
                break;
            case 1:
                //Set Map Type
                level.mapType = LevelGenerator.MapType.random;
                mapSeed.gameObject.SetActive(false);
                break;
            case 2:
                //Set Map Type and Seed
                level.mapType = LevelGenerator.MapType.custom;
                mapSeed.gameObject.SetActive(true);
                if (mapSeed.text == null) { level.mapSeed = 0000; }
                else { level.mapSeed = int.Parse(mapSeed.text); }                
                break;       
        }
        //Set volume sliders
        GameManager.instance.sfxVolume = sfxVolume.value;
        bgm.volume = volume.value; 
	}
}
