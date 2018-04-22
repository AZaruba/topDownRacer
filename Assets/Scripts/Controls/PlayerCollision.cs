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
			Vector3 isUp = Vector3.Cross (wallInfo.normal.normalized, oldDir.normalized);
			if (isUp.normalized.y == Vector3.up.y)
				return newDirection;
			else if (isUp == Vector3.zero)
				return Vector3.zero;
			else
				return newDirection * -1;
		}
		return oldDir.normalized;
    }

	public float getResultVelocity(Vector3 originalDir)
	{
        if (Mathf.Abs(Vector3.Angle(wallInfo.normal.normalized, originalDir)) < 90.0f)
            return 0.0f;
		return Vector3.Magnitude(originalDir.normalized + wallInfo.normal.normalized);
	}

	public bool getCollided()
	{
		return collided;
	}

	public void wallCollision(Vector3 pos, Vector3 dir, Vector3 dimensions, Quaternion orientation, float distanceFromCenter)
    {
		collided = false;

        //dimensions.z = 0.01f;
		if (Physics.BoxCast(pos,dimensions,dir,out wallInfo,orientation,1,colMask))
        {
            dir += wallInfo.normal.normalized;
			newDirection = Vector3.Cross(Vector3.up, wallInfo.normal.normalized);
			collided = true;
        }

		drawPoint ();
    }

	public Vector3 getPoint(Vector3 currentPosition)
	{
		if (collided)
		    return wallInfo.point;
		else
			return currentPosition;
	}

	void Update () {
		
	}

	/*
	 * DEBUG FUNCTIONS: only call these functions when debugging collision quirks
	 */

	void drawPoint()
	{
		Debug.DrawRay (wallInfo.point, wallInfo.normal.normalized);
	}
}
