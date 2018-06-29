using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Robo.Board{
	[ExecuteInEditMode]
	[SelectionBase] //not sure why this is here
	[RequireComponent(typeof(Waypoint))]
	public class GridEditor : MonoBehaviour {
		
		void Update ()
        {
            SnapToGrid();
            SetName();
        }

        protected void SetName()
        {
			string name = transform.position.ToString();
			IObstacle obstacle = gameObject.GetComponent<IObstacle>();
			if (obstacle != null){
				name = obstacle.GetObstacleName() + " " + name;
			}
            gameObject.name = name;
        }

        void SnapToGrid ()
		{
			transform.position = new Vector3 (
				Mathf.RoundToInt (transform.position.x), 
				0f, 
				Mathf.RoundToInt (transform.position.z)
			);
		}
	}
}