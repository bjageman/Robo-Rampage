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
				BotMovement possibleBotCollide = CheckForBotCollision(currentBot);
				if (possibleBotCollide != null){
					print("Bot collided with " + possibleBotCollide );
				}
				
			}
		}

		//TODO May need to move this elsewhere
		BotMovement CheckForBotCollision(BotMovement currentBot){
			List<BotMovement> bots = new List<BotMovement>(); 
			bots.AddRange(FindObjectsOfType<BotMovement>());
			bots.Remove(currentBot);
			foreach(BotMovement bot in bots){
				if (bot.CurrentWaypoint == currentBot.CurrentWaypoint){
					return bot;
				}
			}
			return null;
		}

	}

	//TODO Consider making scriptableobjects
	interface IObstacle
	{
		string GetObstacleName();
    	void endTurnTrigger(BotMovement Bot);
	}
}
