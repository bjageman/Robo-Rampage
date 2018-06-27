using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

namespace Robo.Commands{
	public class MovePipeline : MonoBehaviour {

		[SerializeField] List<CardConfig> cards;
		public bool commandRunning = false;
		
		public List<CardConfig> getCards(){
			return cards;
		}

		public void setCards(List<CardConfig> cardsToSet){
			cards = cardsToSet;
		}

		void Start() {
			//TODO  Get Audio Source
		}

		//TODO I need to be able to run a command, wait for it to finish, then run the next.
		public void RunCommand(int cardIndex, BotMovement bot){
			cards[cardIndex].AttachAbilityTo(bot.gameObject);
			cards[cardIndex].Use(bot);
		}
	}
}