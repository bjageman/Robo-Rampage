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

		//TODO Move collided bot in front of current bot
		void OnTriggerEnter(Collider other) {
			BotMovement currentBot = other.GetComponent<BotMovement>();
			if (currentBot){
				currentBot.CurrentWaypoint = this;
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
