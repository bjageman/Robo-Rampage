using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

namespace Robo.Board{
	[SelectionBase]
	public class Checkpoint : Waypoint, IObstacle {

		BotMovement bot;

		public string GetObstacleName(){
			return "Checkpoint";
		}

		public void endTurnTrigger(BotMovement bot){
			bot.GetComponent<Score>().CollectCheckpoint(this);
			TurnManager turnManager = FindObjectOfType<TurnManager>();
			turnManager.AddPlayerToQueue(bot);
		}
	}
}
