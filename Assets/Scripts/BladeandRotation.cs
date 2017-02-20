using UnityEngine;
using System.Collections;

public class BladeandRotation : MonoBehaviour {
    public Character parent;
    public float radius;
    public float rotationoffset;
    public bool isSwingin = false;
    public bool isStoppin = false;

	// Use this for initialization
	void Start () {
        //move to the parent objects position, and startng position
        rotationoffset=0;
        transform.position = parent.position;
        transform.rotation = Quaternion.Euler(0,0,parent.mouseAngle-90);
        
	}
	
	// Update is called once per frame
	void Update () {
        if(isStoppin)
        {//Will allow code to be run when the blade stops swinging, like score multipliers
            isStoppin = false;
        }

        if (!isSwingin)
        {//if we aren't currently swinging the blade, then just stick with the character model.
            transform.position = parent.position;
            transform.rotation = Quaternion.Euler(0, 0, parent.mouseAngle - 90);


            if (Input.GetMouseButton(0))
            {//if we hit LMB, switch to the swingign state
                if (parent.canMove)
                {//but only if we are allowed to move can we swing
                    Debug.Log("Start to swing");
                    isSwingin = true;
                }
            }
        }//end of base mode
        if (isSwingin)
        {
            if (rotationoffset <= 180)
            {//if we haven't moved to our endpoint yet
                //move the blade to the characters position, then start to rotate it each frame
                transform.position = parent.position;
                transform.rotation = Quaternion.Euler(0, 0, (parent.mouseAngle - 90 + rotationoffset));
                rotationoffset += (Time.deltaTime * 270);
            }
            else
            {//otherwise Reset the rotation amount and turn off swing.
                rotationoffset = 0;
                transform.position = parent.position;
                transform.rotation = Quaternion.Euler(0, 0, (parent.mouseAngle - 90));
                isSwingin = false;
                isStoppin = true;
            }

        }




    }
}
