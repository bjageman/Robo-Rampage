using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots; 

namespace Robo.Board{
	[SelectionBase]
	public class Waypoint : MonoBehaviour {

		float waypointSize = 0.1f;

		public Vector2Int GetGridPosition(){
			return new Vector2Int (
				Mathf.RoundToInt (transform.position.x),
				Mathf.RoundToInt (transform.position.z)
			);
				
		}

		public void OnDrawGizmos(){
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, waypointSize);
		}

		void OnTriggerEnter(Collider other) {
			BotMovement bot = other.GetComponent<BotMovement>();
			if (bot){
				bot.SetCurrentWaypoint(this);
			}
		}

	}

	//TODO Consider making scriptableobjects
	interface IObstacle
	{
		string GetObstacleName();
    	void endTurnTrigger(BotMovement Bot);
	}
}
