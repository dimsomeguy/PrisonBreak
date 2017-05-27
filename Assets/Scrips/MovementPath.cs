using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour {

    public enum PathTypes
    {
        linear,
        loop
    }

    public PathTypes type;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathSeq;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //draw them gizmos
    public void OnDrawGizmos()
    {
        if (PathSeq == null || PathSeq.Length < 2)
            return;

        for (int i = 1; i < PathSeq.Length; i++)
        {
            Gizmos.DrawLine(PathSeq[i -1].position, PathSeq[i].position);
        }

        if (type == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSeq[0].position, PathSeq[PathSeq.Length - 1].position);
        }
    }

    //get next point of transofrm for pathing
    public IEnumerator<Transform> GetNextPathPoint()
    {
        //does the object have a seq
        if (PathSeq == null || PathSeq.Length < 1)
            yield break;
        //if not or it is a stachatory position return


        while (true)
        {
            yield return PathSeq[movingTo];

            if (PathSeq.Length == 1)
            {
                continue;
            }

            if (type == PathTypes.linear)
            {
                if (movingTo <= 0)
                    movementDirection = 1;
                else if (movingTo >= PathSeq.Length - 1)
                    movementDirection = -1;
                
                movingTo = movingTo + movementDirection;

                if (type == PathTypes.loop)
                {
                    if (movingTo >= PathSeq.Length)
                        movingTo = 0;
                    
                    if (movingTo < 0)
                        movingTo = PathSeq.Length - 1;
                    
                }
            }
        } 
    }

}
