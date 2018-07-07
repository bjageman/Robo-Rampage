using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;
using Robo.Cards;

namespace Robo.Board{
	public class LaserGrid : Waypoint, IObstacle {
		[SerializeField] CardConfig spamCard;

		public string GetObstacleName(){
			return "Laser";
		}

		public void endTurnTrigger(BotMovement bot){
			TurnManager turnManager = FindObjectOfType<TurnManager>();
			Deck deck = bot.GetComponent<Deck>();
			deck.DiscardCard(spamCard);
			turnManager.AddPlayerToQueue(bot);
		}
	}
}