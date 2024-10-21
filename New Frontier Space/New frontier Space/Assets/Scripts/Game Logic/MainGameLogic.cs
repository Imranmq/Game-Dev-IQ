using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameLogic : MonoBehaviour
{
    // DATA LISTS    
    List<MapObject> spaceUniverseMap;
    List<Attachment> allAvailableAttachments;

    // PREFABS OR PUBLIC GAME OBJECT ASSIGNED
    public GameObject MainCamera;
    public GameObject MainPlayerPrefab;
    public GameObject hardPointPrefab;
    public GameObject Shape;
    public GameObject Attachment;
    // Player In Database
    PlayerStructure myPlayer1 = new PlayerStructure(); // CREATE OUR PLAYER IN MEMORY
    // GENERATED PRIVATE GAME OBJECT
    GameObject MainPlayer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        // INITIATE DATA STRUCTURE CLASSES
        spaceUniverseMap = new MapStructure().GetMapUniverse();
        Debug.Log(spaceUniverseMap);
        allAvailableAttachments = new AttachmentStructure().GetAttachmentsList();
        Debug.Log(allAvailableAttachments);
        // IF PLAYER ISNT SPAWNED YET IN START , INSTANTIATE IT
        if (!MainPlayer)
        {
            // PLAYER START POSITION
            Vector3 playerPos = myPlayer1.PlayerShip.GetShipPositions();
            playerPos.x = 50;
            playerPos.y = -40;
            playerPos.z = 0;
            // INSTANTIATE PLAYER
            MainPlayer = Instantiate(MainPlayerPrefab, playerPos, Quaternion.identity);
            
            // SET PLAYER TO MAIN CAMERA SO IT CAN CALCULATE OFFSET AND FOLLOW THE PLAYER
            // SET PLAYER TO THE PLAYER MOVEMENT SCRIPT TO BE ABLE TO SYNC POSITIONS BETWEEN THEM
            MainPlayer.GetComponent<PlayerMovement>().SetPlayerClass(myPlayer1);
            myPlayer1.PlayerShip.SetShipGameObject(MainPlayer);
            MainCamera.GetComponent<FollowPlayer>().SetPlayer(MainPlayer);            
            // MAIN PLAYER SHIP SETS THE WEAPONS TYPE 0 (LASERS) FOR NOW        
            foreach (var attachment in allAvailableAttachments)
            {
                Type type = attachment.GetType();
                Type checkMineType = typeof(MiningLaser);
                Type checkBoosterType = typeof(Thruster);
                bool foundMineType = type.Equals(checkMineType);
                bool foundBoosterType = type.Equals(checkBoosterType);
                if (foundMineType)
                {
                    MiningLaser minLaser = attachment as MiningLaser;
                    if (minLaser.weaponType == 1 && minLaser.tier =="T1")
                    {
                        myPlayer1.PlayerShip.AddAttachment(minLaser);
                    }
                }
                if (foundBoosterType)
                {
                    Thruster booster = attachment as Thruster;
                    if(booster.tier == "T1")
                    {
                        myPlayer1.PlayerShip.AddAttachment(booster);
                    }
                }                
            }

            //MainPlayer.GetComponent<PlayerClass>().SyncWithPlayer(myPlayer1);
        }
    }      
    
    // TO BE DISCUSSED
    public int GetOreGeneration()
    {
        return gameObject.GetComponent<AsteroidFieldSpawnParamters>().GetOreGeneration();
    }
}
