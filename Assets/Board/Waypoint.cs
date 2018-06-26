using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Commands; //TODO Consider refactor

namespace Robo.Board{
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
}
