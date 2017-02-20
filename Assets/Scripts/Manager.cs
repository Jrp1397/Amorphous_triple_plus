using UnityEngine;
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
    public List<EnemyParent> droneList;
    public GameObject seekerPrefab;
    public int maxDrones;
    public int DroneSpawnChance;
    public int maxSeekers;

    //character stuff
    public Character mainCharacter;
    public BladeandRotation greatsword;



	// Use this for initialization
	void Start () {
	for(int i=0;  i<(maxDrones + maxSeekers); i++)
        {
            holding = Instantiate(dronePrefab);
            droneList.Add(holding.GetComponent<EnemyParent>());
            droneList[i].Disable();
            droneList[i].Setup(mainCharacter);
            if (i > maxDrones)
            {
                holding = Instantiate(seekerPrefab);
                droneList.Add(holding.GetComponent<SeekingChild>());
                droneList[i].Disable();
                droneList[i].Setup(mainCharacter);
            }
            Debug.Log("Debug: added enemy to preconstructed list.");
        }
    }
	
	// Update is called once per frame
	void Update () {
        DroneUpdate();
        CheckCollisions();
        ChangeScore();
	}

    public void DroneUpdate()
    {// for each type of enemy, go through all of em, and turn on some of them if they are off.
        foreach (EnemyParent drone in droneList)
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
        foreach (EnemyParent drone in droneList)
        {
            if (drone.isActive)
            {
                if ((((mainCharacter.position.x - drone.position.x)* (mainCharacter.position.x - drone.position.x)) + ((mainCharacter.position.y - drone.position.y) * (mainCharacter.position.y - drone.position.y)))<(drone.radius*drone.radius))
                {
                    Debug.Log("player hit");
                    if (drone.DamageType == 1)
                    {
                        score = 0;
                        Debug.Log("You died");
                    }
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
