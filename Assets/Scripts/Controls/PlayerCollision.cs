using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public int collisionMask;
    private int colMask;
    private RaycastHit wallInfo;

	void Start () {
        colMask = 1 << collisionMask; // use a public value for naming the layer, but a private value to ensure it WORKS
	}

	public Vector3 wallCollision(Vector3 pos, Vector3 dir, float distanceFromCenter, float velocity)
    {

        /* Given a position defined as DEAD CENTER of the object
         * boxEdgePoint will be a vector pointing in the direction the object is traveling in
         * with a magnitude equal to the distance from the center point (position)
         * 
         * So when we translate pos by this vector, it will be the point coming out of the front of the box 
         */
        Vector3 boxEdgePoint = dir * distanceFromCenter;

        pos += boxEdgePoint;

        if (Physics.Raycast(pos, dir, out wallInfo, 1/*,colMask*/))
        {
            dir += wallInfo.normal.normalized; // the result of this addition is the new direction
        }

        return dir * velocity;
    }

	void Update () {
		
	}
}
