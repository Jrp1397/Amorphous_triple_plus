using UnityEngine;
using System.Collections;


public class Blob : MonoBehaviour {
    public GameObject bumper;
    public int determinator;
    public float radius;
    public Vector3 worldSize;
    public float rotAngle;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 position;
    public bool isActive;


	// Use this for initialization
	void Start () {

      
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            DisableBoundry();
            position += velocity * Time.deltaTime;
            bumper.transform.position = position;
        }
    }

    public void Generate()
    {
         determinator = Random.Range(0, 4);//put the object on a wall, and give it a forward facing direction
        if(determinator == 0)
        {
            position.x = -(worldSize.x);
            position.y = (Random.Range(-worldSize.y, worldSize.y));
            rotAngle = (Random.Range(0, 180) - 90);          
        }
        if (determinator == 1)
        {
            position.x = (Random.Range(-worldSize.x, worldSize.x));
            position.y = -worldSize.y;
            rotAngle = (Random.Range(0, 180));
        }
        if (determinator == 2)
        {
            position.x = worldSize.x;
            position.y = (Random.Range(-worldSize.y, worldSize.y));
            rotAngle = (Random.Range(90, 270));
        }
        if (determinator == 3)
        {
            position.x = (Random.Range(-worldSize.x, worldSize.x));
            position.y = worldSize.y;
            rotAngle = (Random.Range(180, 360));
        }

        bumper.transform.position = position;
        rotation = Quaternion.Euler(0, 0, rotAngle);
        bumper.transform.rotation = rotation;

        Vector3 forward = (bumper.transform.right);
        velocity = forward;

    }

    void DisableBoundry()
    {
        if(position.x > worldSize.x || position.x < -worldSize.x || position.y > worldSize.y || position.y < -worldSize.y) {             
            position.x = 20;
            isActive = false;
        }
    }
    public void Disable()
    {
        position.x = 20;
        bumper.transform.position = position;
        isActive = false;
    }
}
