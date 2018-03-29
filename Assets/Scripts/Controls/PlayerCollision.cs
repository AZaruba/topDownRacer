using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public int collisionMask;
    private int colMask;
    private RaycastHit wallInfo;
    private Vector3 newDirection;

	void Start () {
        colMask = 1 << collisionMask; // use a public value for naming the layer, but a private value to ensure it WORKS
	}

    public Vector3 getNewDirection()
    {
        return newDirection;
    }

	public Vector3 wallCollision(Vector3 pos, Vector3 dir, Vector3 dimensions, Quaternion orientation, float distanceFromCenter, float velocity)
    {

        /* Given a position defined as DEAD CENTER of the object
         * boxEdgePoint will be a vector pointing in the direction the object is traveling in
         * with a magnitude equal to the distance from the center point (position)
         * 
         * So when we translate pos by this vector, it will be the point coming out of the front of the box 
         */
        //Vector3 boxEdgePoint = dir * distanceFromCenter;

        //pos += boxEdgePoint;

        dimensions.z = 0.001f;
        Debug.Log(distanceFromCenter);
        if (Physics.BoxCast(pos,dimensions,dir,out wallInfo,orientation,1,colMask))
        {
            dir += wallInfo.normal.normalized;
            newDirection = Vector3.Cross(wallInfo.normal.normalized, Vector3.up.normalized);
        }

        return dir * velocity;
    }

	void Update () {
		
	}
}
