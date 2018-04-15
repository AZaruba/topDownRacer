using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public int collisionMask;
    private int colMask;
    private RaycastHit wallInfo;
    private Vector3 newDirection;
	private bool collided;

	void Start () {
        colMask = 1 << collisionMask; // use a public value for naming the layer, but a private value to ensure it WORKS
		collided = false;
	}

	public Vector3 getNewDirection(Vector3 oldDir)
    {
		// TODO: figure out an elegant solution for deciding WHICH normal we're going with, this doesn't work
		if (collided) {
			Vector3 isUp = Vector3.Cross (oldDir.normalized, newDirection.normalized);
			if (isUp.normalized.y == Vector3.up.y)
				return newDirection;
			else
				return newDirection * -1;
		}
		return oldDir.normalized;

		// Why, though? Taking the cross product of our old direction and new direction should provide
		// either up or down (the game is designed with the assumption that all colliders are flat walls)
		// but for some reason it only wants to move up the X-axis
    }

	public void wallCollision(Vector3 pos, Vector3 dir, Vector3 dimensions, Quaternion orientation, float distanceFromCenter, float velocity)
    {
		collided = false;

        dimensions.z = 0.001f;
		if (Physics.BoxCast(pos,dimensions,dir,out wallInfo,orientation,1,colMask))
        {
            dir += wallInfo.normal.normalized;
			newDirection = Vector3.Cross(Vector3.up, wallInfo.normal.normalized);
			collided = true;
			Debug.Log (dir.normalized);
        }
			
    }

	void Update () {
		
	}
}
