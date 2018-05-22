using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {


	public Vector2Int GetGridPosition(){
		return new Vector2Int (
			Mathf.RoundToInt (transform.position.x),
			Mathf.RoundToInt (transform.position.z)
		);
			
	}
}
