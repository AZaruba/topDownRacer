﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public Vector3 direction;
    public float angle;
	public float turn;

    public float topSpeed;
	public float boost;
    public float acceleration;
    public float boostDeceleration;
    public float handling;
	public float driftHandling;
    public float braking;

    private float velocity;
	private bool drifting;
    private TimeKeeper clock;
    private PlayerCollision playerCollider;

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

        clock = gameObject.GetComponent(typeof(TimeKeeper)) as TimeKeeper;
        playerCollider = gameObject.GetComponent(typeof(PlayerCollision)) as PlayerCollision;
	}

    void accelerate()
    {
        if (velocity < topSpeed && !Input.GetKey("space"))
            velocity += acceleration * Time.deltaTime;
        else if (velocity > topSpeed)
        {
            velocity -= boostDeceleration * Time.deltaTime;
        }
        if (Input.GetKey("space"))
        {
            velocity -= braking * (topSpeed / velocity) * Time.deltaTime;
            if (velocity < 0.0f)
                velocity = 0.0f;
        }
    }

    void handle()
    {
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, handling * Time.deltaTime * 10, 0);
            angle += turn * Mathf.PI / 180 * Time.deltaTime * 10;
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -1 * handling * Time.deltaTime * 10, 0);
            angle -= turn * Mathf.PI / 180 * Time.deltaTime * 10;
        }

        // TODO: handling vertical translation
        direction.x = Mathf.Sin(angle) * velocity;
        direction.z = Mathf.Cos(angle) * velocity;
    }

    void drift()
    {
        if (Input.GetKeyDown("space"))
        {
            turn = driftHandling;
            drifting = true;
            clock.startTimer();
        }
        else if (!Input.GetKey("space") && drifting == true)
        {
            turn = handling;
            float boostRatio = 0.0f;
            if (clock.isActive())
                boostRatio = clock.stopTimer() / clock.getMaxTime();
            else
                boostRatio = clock.stopTimer();
            float eulerReturn = transform.rotation.eulerAngles.y;
            angle = eulerReturn * Mathf.PI / 180;
            velocity += boost * boostRatio;
            drifting = false;
        }
    }

    void checkForCollision()
    {
        BoxCollider colliderInfo = gameObject.GetComponent<BoxCollider>();
        if (colliderInfo == null)
            return;

        Quaternion orientation = transform.rotation;

        Vector3 dimensions = (colliderInfo.size) / 2;
        Debug.Log(dimensions);
        float colliderDist = dimensions.z;
        Vector3 resultDirection = playerCollider.wallCollision(transform.position, direction.normalized, dimensions, orientation, colliderDist, velocity);
        Vector3 resultOrientation = playerCollider.getNewDirection();

        // float newAngle = Mathf.Acos(Vector3.Dot(direction.normalized, resultOrientation.normalized));

        direction.x = resultDirection.x;
        direction.z = resultDirection.z;
        // transform.Rotate(0, newAngle, 0);
    }
	
	// Update is called once per frame
	void Update ()
	{
        accelerate();
        drift();
        handle();
        checkForCollision();

        transform.Translate(direction * Time.deltaTime, Space.World);
	}
}
