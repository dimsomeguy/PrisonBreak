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
            Debug.LogError("A Path must ahve points in it to follow", gameObject);
            return;
        }

        transform.position = PointInPath.Current.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PointInPath == null || PointInPath.Current == null)
        {
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

        if (distence < MaxDistenceToGoal * MaxDistenceToGoal)
        {
            PointInPath.MoveNext();
        }
	}
}
