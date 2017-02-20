using UnityEngine;
using System.Collections;

public class EnemyParent : MonoBehaviour {

    public GameObject bumper;
    public int DamageType;
    public int determinator;
    public float radius;
    public Vector3 worldSize;
    public float rotAngle;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 position;
    public bool isActive;

    // Use this for initialization
     public virtual void Start () {
        DamageType = 0;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (isActive)
        {
            DisableBoundry();
            position += velocity * Time.deltaTime;
            bumper.transform.position = position;
        }
    }

    public virtual void Generate() {
        determinator = Random.Range(0, 4);//put the object on a wall, and give it a forward facing direction
        if (determinator == 0)
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
    
    public virtual void DisableBoundry()
    {
        if (position.x > worldSize.x || position.x < -worldSize.x || position.y > worldSize.y || position.y < -worldSize.y)
        {
            position.x = 20;
            isActive = false;
        }
    }
    public virtual void Disable()
    {
        position.x = 20;
        bumper.transform.position = position;
        isActive = false;
    }
    public virtual void Setup(Character MC)
    {
        Debug.Log("i'm a placeholder");
    }
}
