﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour{
    //Variables
    public int rows;                                        //The amount of rows in the level
    public int cols;                                        //The amount of collumns in the level
    private float roomWidth = 50.0f;                        //The width of each room
    private float roomHeight = 50.0f;                       //The height of each room
    private Room[,] grid;                                   //The list of rooms in our level
    public GameObject[] gridPrefabs;                        //The list of tiles to build our level
    public enum MapType { mapOfTheDay, random, custom};     //What seed to use for the map
    public MapType mapType;                                 //The current type of seed to use
    public int mapSeed;                                     //The seed for the randomly generated level
    private SpawnPlayer playerSpawn;                        //SpawnPlayer Component

    // Use this for initialization
    void Start()
    {
        //Get Components
        playerSpawn = GetComponent<SpawnPlayer>();

        //Set the Seed
        switch (mapType)
        {
            case MapType.mapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;

            case MapType.random:
                mapSeed = DateToInt(DateTime.Now);
                break;

            case MapType.custom:
                //Set the seed in the inspector
                break;
        }

        //Generate Grid
        GenerateGrid();

        //Set Player Spawns
        playerSpawn.setSpawns();

    }

    public void GenerateGrid()
    {
        //Set our seed
        UnityEngine.Random.seed = mapSeed;

        //Clear out the grid
        grid = new Room[rows, cols];

        //For each grid row...
        for (int i = 0; i < rows; i++)
        {
            //For each column in that row
            for (int j = 0; j < cols; j++)
            {
                //Figure out the location. 
                float xPosition = roomWidth * j;
                float zPosition = roomHeight * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                //Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set its parent
                tempRoomObj.transform.parent = this.transform;

                //Give it a meaningful name
                tempRoomObj.name = "Room_" + j + "," + i;

                //Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //If we are on the bottom row, open the north door
                if (i == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (i == rows - 1)
                {
                    //Otherwise, if we are on the top row, open the south door
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    //Otherwise, we are in the middle, so open both doors
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                //If we are on the first column, open the east door
                if (j == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (j == cols - 1)
                {
                    //Otherwise, if we are on the last column row, open the west door
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    //Otherwise, we are in the middle, so open both doors
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                //Save it to the grid array
                grid[j, i] = tempRoom;
            }
        }
    }

    //Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public int DateToInt(DateTime dateToUse)
    {
        //Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

}
