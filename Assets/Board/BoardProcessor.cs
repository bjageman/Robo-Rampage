using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardProcessor : MonoBehaviour {

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

	public Dictionary<Vector2Int, Waypoint> GetGrid(){
		return grid;
	}

	public Waypoint GetWaypoint(Vector2Int currentPosition){
		Waypoint[] waypoints = FindObjectsOfType<Waypoint> ();
		foreach (Waypoint waypoint in waypoints) {
			if (waypoint.GetGridPosition () == currentPosition) {
				return waypoint;
			}
		}
		Debug.LogWarning ("Waypoint Not Found");
		return null;
	}

	private void LoadBoard(){
		Waypoint[] waypoints = FindObjectsOfType<Waypoint> ();
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
