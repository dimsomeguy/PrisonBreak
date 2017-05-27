using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }


    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float Speed = 1f;
    public float MaxDistenceToGoal = .1f;

    private IEnumerator<Transform> PointInPath;
	// Use this for initialization
	void Start () {
        if (MyPath == null) {
            Debug.Log("there is no path to follow please assind to");
            return;
        }

        PointInPath = MyPath.GetNextPathPoint();

        PointInPath.MoveNext();

        if (PointInPath.Current == null)
        {
            Debug.LogError("A Path must have points in it to follow", gameObject);
            return;
        }

        transform.position = PointInPath.Current.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if path is emepty or there is no path point return 
        if (PointInPath == null || PointInPath.Current == null)
        {
            Debug.LogError("there is no point to follow", gameObject);
            return;
        }

        
        if (Type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointInPath.Current.position, Time.deltaTime * Speed);
        }
        else if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, PointInPath.Current.position, Time.deltaTime * Speed);
        }

        
        float distence = (transform.position - PointInPath.Current.position).sqrMagnitude;

        Debug.Log("current pos: " + transform.position  + "next position" + PointInPath.Current.position);
        if (distence < MaxDistenceToGoal * MaxDistenceToGoal)
        {
            PointInPath.MoveNext();
        }
	}
}
