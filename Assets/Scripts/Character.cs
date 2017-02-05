using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {
    public GameObject mainCharacter;
    public BladeandRotation greatsword;
    public Vector3 worldSize;
    public Vector3 mousePosition;
    public Vector3 position;
    public Vector3 forward;
    public Vector3 backward;
    public Vector3 leftward;
    public Vector3 rightward;
    public Vector3 velocity;
    public Quaternion rotation;
    public float mouseAngle;
    public enum direction {W, S, A, D, WA, WD, SA, SD}
    public direction lastDirection;
    public bool canMove = true;
    float rollDelay = .6f;
    float curRollDelay;

	// Use this for initialization
	void Start () {
        position = mainCharacter.transform.position;
        lastDirection = direction.D;
        curRollDelay = rollDelay;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        EdgeBoundry();
       

	}
    public void Movement()
    {
        position = mainCharacter.transform.position;
        if (greatsword.isSwingin)
        {//if we are swing sword, we cannot move
            canMove = false;
        }

        if (canMove)//if we aren't doing something that prevents movement
        {
            //Rotate the character towards the mouse
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseAngle = Mathf.Atan2((position.y - mousePosition.y), (position.x - mousePosition.x));
            mouseAngle *= (180 / Mathf.PI);
            mouseAngle -= 90;
            rotation = Quaternion.Euler(0, 0, mouseAngle);
            mainCharacter.transform.rotation = rotation;


            //create relative directions

            leftward = (mainCharacter.transform.right);
            rightward = new Vector3(-leftward.x, -leftward.y, 0);
            forward = new Vector3(leftward.y, -leftward.x, 0);
            backward = new Vector3(-leftward.y, leftward.x, 0);

            if (Input.GetKey(KeyCode.Space))
            {//if you hit space, start a combat roll
                canMove = false;
                curRollDelay = 0;
            }
            if (Input.GetKey(KeyCode.A))//If you hit a direction arrow, move that way, and set your current direction(s)
            {
                velocity += leftward * 2;
                lastDirection = direction.A;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity += rightward * 2;
                lastDirection = direction.D;
            }
            if (Input.GetKey(KeyCode.W))
            {
                velocity += forward * 2;
                lastDirection = direction.W;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity += backward * 2;
                lastDirection = direction.S;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                lastDirection = direction.WA;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                lastDirection = direction.WD;
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                lastDirection = direction.SA;
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                lastDirection = direction.SD;
            }
        }
        else//will have to add code for different can't move states
        {
            //Rolling
            if (curRollDelay < rollDelay)
            {//if we're still rolling
                if (lastDirection == direction.W || lastDirection == direction.WA || lastDirection == direction.WD)
                {//Roll in the proper direction, based on last dirtection moved
                    velocity += forward * 4;
                }
                if (lastDirection == direction.A || lastDirection == direction.WA || lastDirection == direction.SA)
                {
                    velocity += leftward * 4;
                }
                if (lastDirection == direction.S || lastDirection == direction.SD || lastDirection == direction.SA)
                {
                    velocity += backward * 4;
                }
                if (lastDirection == direction.D || lastDirection == direction.WD || lastDirection == direction.SD)
                {
                    velocity += rightward * 4;
                }
            }
            else
            {//if we aren't rolling, 
                if (greatsword.isSwingin == false)
                {//and we aren't swinging, then we can move
                    canMove = true;
                }
            }

            curRollDelay += Time.deltaTime;

        }
        position += velocity * Time.deltaTime;
        mainCharacter.transform.position = position;
        velocity = Vector3.zero;
    }

    public void EdgeBoundry()
    {//if the position is outsideo f the worldsize, put it back in that position.
        if(position.x > worldSize.x) 
        {
            position.x = worldSize.x;            
        }
        if (position.x < -worldSize.x)
        {
            position.x = -worldSize.x;
        }
        if (position.y > worldSize.y)
        {
            position.y = worldSize.y;
        }
        if (position.y < -worldSize.y)
        {
            position.y = -worldSize.y;
        }
        mainCharacter.transform.position = position;
    }
}
