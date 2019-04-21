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
    public List<AudioSource> bgm;       //The BGM source
    private LevelGenerator level;       //The level

	// Use this for initialization
	void Start () {
        //Get Components
        level = GameManager.instance.level.GetComponent<LevelGenerator>();

    }
	
	// Update is called once per frame
	void Update () {
		
        switch (playerSelect.value)
        {
            case 0:
                //Set Multiplayer
                level.multiplayer = false;
                //Set Screens
                GameManager.instance.gameUI1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (Screen.height));
                GameManager.instance.gameUI2.SetActive(false);
                break;
            case 1:
                //Set Multiplayer
                level.multiplayer = true;
                //Set Screens
                GameManager.instance.gameUI1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height/2);
                GameManager.instance.gameUI2.SetActive(true);
                GameManager.instance.gameUI2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height/2);
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
        for(int i = 0; i < bgm.Count; i++)
        {
            bgm[i].volume = volume.value;
        }
	}
}
