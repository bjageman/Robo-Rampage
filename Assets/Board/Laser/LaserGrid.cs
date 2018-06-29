using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using Robo.Commands;

namespace Robo.Board{
	public class LaserGrid : Waypoint, IObstacle {
		[SerializeField] int moveSpaces = 1;
		[SerializeField] Vector2Int direction = new Vector2Int(0,1);

		[SerializeField] CardConfig spamCard;

		public string GetObstacleName(){
			return "Laser";
		}

		public void endTurnTrigger(BotMovement bot){
			print("Hit by laser");
			Deck deck = bot.GetComponent<Deck>();
			deck.DiscardCard(spamCard);
		}
	}
}