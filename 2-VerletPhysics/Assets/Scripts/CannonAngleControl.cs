using UnityEngine;
using System.Collections;

public class CannonAngleControl : MonoBehaviour {

	public static int speed; // cannon speed
	public static float angle; // cannon angle
    public Quaternion currentAngle;



    // Use this for initialization
    void Start () {
	
		speed = 50; // initialize speed
        currentAngle = transform.rotation;


    }

    // Update is called once per frame
    void Update ()
    {
	
		float deltaMovement = 0.3f; 

		transform.Rotate(Vector3.forward, Input.GetAxis("Vertical") * deltaMovement);
        currentAngle = transform.rotation;
		transform.eulerAngles = new Vector3(0.0f,180.0f, Mathf.Clamp(transform.rotation.eulerAngles.z, 1.0f, 100.0f));

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			speed--;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			speed++;
		}

		speed = Mathf.Clamp(speed, 0, 100);


    }
}
