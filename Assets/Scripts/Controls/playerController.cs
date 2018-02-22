using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public Vector3 direction;
    public float angle;

    public float topSpeed;
    public float acceleration;
    public float handling;
    public float braking;

    private float velocity;

	// Use this for initialization
	void Start () {
        velocity = 0.0f;
        angle = 0.0f;

        direction.x = 1.0f;
        direction.y = 0.0f;
        direction.z = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        direction.x = Mathf.Sin(angle);
        direction.z = Mathf.Cos(angle);
        if (velocity < topSpeed && !Input.GetKey("space"))
        {
            velocity += acceleration;
        }
        if (Input.GetKey("space") && velocity > 0.0f)
        {
            velocity -= braking;
            if (velocity < 0.0f)
                velocity = 0.0f;
        }

        if (Input.GetKey("d"))
            angle += handling * Mathf.PI / 180;

        if (Input.GetKey("a"))
            angle -= handling * Mathf.PI / 180;

        direction.x *= velocity;
        direction.z *= velocity;

        transform.Translate(direction * Time.deltaTime, Space.World);
	}
}
