﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
    //All purpose stuff
    public float enemyAngle;
    public GameObject holding;
    public int scoreIncrement=0;
    public int scoreMultiplier = 0;
    public int score = 0;
    //drone stuff
    public GameObject dronePrefab; 
    public List<Blob> droneList;
    public int maxDrones;
    public int DroneSpawnChance;
    //character stuff
    public Character mainCharacter;
    public BladeandRotation greatsword;



	// Use this for initialization
	void Start () {
	for(int i=1;  i<maxDrones; i++)
        {
            holding = Instantiate(dronePrefab);
            droneList.Add(holding.GetComponent<Blob>());
            droneList[i - 1].Disable();
        }

	}
	
	// Update is called once per frame
	void Update () {
        DroneUpdate();
        CheckCollisions();
        ChangeScore();
	}

    public void DroneUpdate()
    {
        foreach (Blob drone in droneList)
        {
            if (!drone.isActive)
            {
                if (Random.Range(0, DroneSpawnChance) < 5)
                {
                    Debug.Log("Activating Drone");
                    drone.isActive = true;
                    drone.Generate();
                }
            }
        }
    }

    public void CheckCollisions()
    {
        foreach (Blob drone in droneList)
        {
            if (drone.isActive)
            {
                if ((((mainCharacter.position.x - drone.position.x)* (mainCharacter.position.x - drone.position.x)) + ((mainCharacter.position.y - drone.position.y) * (mainCharacter.position.y - drone.position.y)))<(drone.radius*drone.radius))
                {
                    Debug.Log("player hit");
                }
                enemyAngle = Mathf.Atan2((drone.position.y - mainCharacter.position.y), (drone.position.x - mainCharacter.position.x));
                enemyAngle *= (180 / Mathf.PI);
                enemyAngle -= 180;
                if(enemyAngle <-270)
                {//adjust angles to equalibrium
                    enemyAngle += 360;
                }
                if (greatsword.isSwingin)
                {//If we are swinging the blade, check to see if its hitting anything
                    if ((enemyAngle > (mainCharacter.mouseAngle + greatsword.rotationoffset - 15) && enemyAngle < (mainCharacter.mouseAngle + greatsword.rotationoffset + 15) && ((((mainCharacter.position.x - drone.position.x) * (mainCharacter.position.x - drone.position.x)) + ((mainCharacter.position.y - drone.position.y) * (mainCharacter.position.y - drone.position.y))) < (greatsword.radius * greatsword.radius))))
                    {//If the angle is with 15 degree, and the distance is within blade reach, and the player is swinging, disable the enemy;
                        Debug.Log("enemy Hit");
                        scoreIncrement += 1;
                        scoreMultiplier += 1;
                        drone.Disable();
                    }
                }
                
            }
        }
    }

    public void ChangeScore()
    {
        if (greatsword.isStoppin)
        {
            score += (scoreIncrement * scoreMultiplier);
            scoreIncrement = 0;
            scoreMultiplier = 0;
        }
    }
}
