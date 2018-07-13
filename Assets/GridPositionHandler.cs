using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robo.Board;

public class GridPositionHandler : MonoBehaviour {
	
	[SerializeField] [Range(.01f, .5f)] public float waypointThreshold = .1f; 

	Waypoint currentWaypoint;
	BoardProcessor board;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<BoardProcessor>();
		currentWaypoint = board.GetNearestWaypoint(transform.position.x, transform.position.z, waypointThreshold);
		transform.position = currentWaypoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
