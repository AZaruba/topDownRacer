using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public Vector2 direction;
    public float topSpeed;
    public float acceleration;
    public float handling;

    private float velocity;

	// Use this for initialization
	void Start () {
        velocity = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition;
        if (velocity < topSpeed)
        {
            velocity += acceleration;
        }


		// two control types: A and D to turn and WASD to orient, spacebar is always charge
        // We should use one master update function that grabs each player controller object's input
        //    - so if SPACEBAR is held (or A or whatever) we should make a point
        //    - if turning input is held, make adjustments
        // this function simply grabs all player inputs and sends them to the master update function

        // this class should hold the controller input which this player is taking from (so it can change over time)
        // it should also contain all other control types (menu control, etc)
        // perhaps, as a player controller, the current physics applications (buffs, stats, etc) should also be here?
        // i.e. have a class that contains EVERYTHING corresponding to one human player
	}
}
