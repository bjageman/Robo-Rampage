using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardProcessor : MonoBehaviour {

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

	public Dictionary<Vector2Int, Waypoint> GetGrid(){
		return grid;
	}

	public Waypoint GetNearestWaypoint(Vector2Int waypoint, float threshold = .1f){
		return GetNearestWaypoint(waypoint.x, waypoint.y, threshold);
	}
	public Waypoint GetNearestWaypoint(float positionX, float positionY, float threshold = .1f){
		Vector2Int waypointPosition = new Vector2Int (
			Mathf.RoundToInt(positionX), 
			Mathf.RoundToInt(positionY));
		Waypoint[] waypoints = GetComponentsInChildren<Waypoint> ();
		foreach (Waypoint waypoint in waypoints) {
			float distanceBetweenWaypoints = (waypoint.GetGridPosition () - waypointPosition).magnitude;
			if (distanceBetweenWaypoints < threshold) {
				return waypoint;
			}
		}
		return null;
	}

	private void LoadBoard(){
		Waypoint[] waypoints = GetComponentsInChildren<Waypoint> ();
		foreach (Waypoint waypoint in waypoints) {
			var gridPosition = waypoint.GetGridPosition ();
			if (grid.ContainsKey (waypoint.GetGridPosition ())) {
				Debug.LogWarning ("Skipping overlapping block " + waypoint);
			} else {
				grid.Add (gridPosition, waypoint);
			}
		}
	}

}
