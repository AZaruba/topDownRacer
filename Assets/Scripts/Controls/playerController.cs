using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

	public Vector3 direction;
	public Vector3 faceDirection;
    public float angle;
	public float turn;

    public float topSpeed;
	public float boost;
    public float acceleration;
    public float handling;
	public float driftHandling;
    public float braking;

    private float velocity;
	private bool drifting;

	// Use this for initialization
	void Start ()
	{
        velocity = 0.0f;
        angle = 0.0f;
		turn = handling;
		drifting = false;

        direction.x = 1.0f;
        direction.y = 0.0f;
        direction.z = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
        direction.x = Mathf.Sin(angle);
        direction.z = Mathf.Cos(angle);
        if (velocity < topSpeed && !Input.GetKey("space"))
        {
            velocity += acceleration;
        }
		if (velocity > topSpeed)
		{
			velocity -= acceleration*2;
		}
		if (Input.GetKey ("space")) {
			velocity -= braking;
			if (velocity < 0.0f)
				velocity = 0.0f;
			turn = driftHandling;
			drifting = true;
		}
		else
		{
			turn = handling;
			if (drifting == true)
			{
				// get the y-rotation of the object and apply it to the direction vector
				float eulerReturn = transform.rotation.eulerAngles.y;
				direction.x = Mathf.Sin(eulerReturn);
				direction.z = Mathf.Cos(eulerReturn);
				velocity = boost; // add a function of time to this
				drifting = false;
			}
		}

		if (Input.GetKey ("d"))
		{
			transform.Rotate (0, handling*Time.deltaTime*10, 0);
			angle += turn * Mathf.PI / 180 * Time.deltaTime * 10;
		}

		if (Input.GetKey ("a"))
		{
			transform.Rotate (0, -1*handling*Time.deltaTime*10, 0);
			angle -= turn * Mathf.PI / 180 * Time.deltaTime * 10;
		}

        direction.x *= velocity;
        direction.z *= velocity;

        transform.Translate(direction * Time.deltaTime, Space.World);
	}
}
