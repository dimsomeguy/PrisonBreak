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
        {
            Debug.Log("length check failed");
            yield break;
        }
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
                //if we are at the biginig 
                if (movingTo <= 0)
                {
                    //move forward 
                    movementDirection = 1;
                }
                else if (movingTo >= PathSeq.Length - 1)
                {
                    //if we are at the end reverse direction 
                    movementDirection = -1;
                   
                }
            }
            //movement Direction should be ether 1 or -1 
            //add direction to the index to move us to the next point in out path
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
