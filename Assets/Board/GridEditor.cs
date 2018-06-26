using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Robo.Board{
	[ExecuteInEditMode]
	[SelectionBase] //not sure why this is here
	[RequireComponent(typeof(Waypoint))]
	public class GridEditor : MonoBehaviour {
		
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
}