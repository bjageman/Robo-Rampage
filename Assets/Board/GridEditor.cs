using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase] //not sure why this is here
[RequireComponent(typeof(Waypoint))]
public class GridEditor : MonoBehaviour {

	Waypoint waypoint;

	void Start(){
		waypoint = GetComponent<Waypoint>();
	}

	void Update () {
		SnapToGrid ();
	}

	void SnapToGrid ()
	{
		transform.position = new Vector3 (
			Mathf.RoundToInt (transform.position.x), 
			0f, 
			Mathf.RoundToInt (transform.position.z)
		);
		gameObject.name = transform.position.ToString();
	}
}
