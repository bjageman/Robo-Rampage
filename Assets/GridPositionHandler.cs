using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robo.Board;

public class GridPositionHandler : MonoBehaviour {
	
	[SerializeField] [Range(.01f, .5f)] public float waypointThreshold = .1f; 

	Waypoint currentWaypoint;
	BoardProcessor board;

	public Waypoint CurrentWaypoint { 
		get { return currentWaypoint;  } 
		set { currentWaypoint = value; }	
	}

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<BoardProcessor>();
		currentWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
		transform.position = currentWaypoint.transform.position;
		transform.parent = currentWaypoint.transform;
	}

	public Waypoint FixPositionToWaypoint()
	{
		var nearestWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
		if (nearestWaypoint == null){
			currentWaypoint = null;
			return null; //DestroyBot();
		}else{
			transform.position = nearestWaypoint.transform.position;
			currentWaypoint = nearestWaypoint;
			transform.parent = currentWaypoint.transform;
			return currentWaypoint;
		}            
	}
}
