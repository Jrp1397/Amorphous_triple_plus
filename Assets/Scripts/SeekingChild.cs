using UnityEngine;
using System.Collections;

public class SeekingChild : EnemyParent {
    public Character mainCharacter;

   

    // Use this for initialization
    public override void Start () {
        DamageType = 1;
	}

	override public void Setup(Character MC)
    {
        mainCharacter = MC;
        Debug.Log("Inheritance worked properly.");
    }
	// Update is called once per frame
	public override void Update () {
        DisableBoundry();

        rotAngle = Mathf.Atan2((position.y - mainCharacter.position.y), (position.x - mainCharacter.position.x));
        rotAngle *= (360 / Mathf.PI);
        rotation = Quaternion.Euler(0, 0, rotAngle);
        bumper.transform.rotation = rotation;

        Vector3 forward = (bumper.transform.right);
        velocity = forward;
        velocity.Normalize();
        position += (velocity * Time.deltaTime * 3);
        bumper.transform.position = position;
        Debug.Log("updated teir 2");
    }
}
