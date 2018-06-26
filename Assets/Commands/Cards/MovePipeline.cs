using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robo.Commands{
	public class MovePipeline : MonoBehaviour {

		[SerializeField] List<CardConfig> cards;
		public bool commandRunning = false;
		
		public List<CardConfig> getCards(){
			return cards;
		}

		void Start() {
			//TODO  Get Audio Source
			// AttachInitialCommands(); //DEBUG ONLY - Remove Later
		}

		//TODO Probably won't need to attach
		private void AttachInitialCommands(){
			for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++){
				cards[cardIndex].AttachAbilityTo(gameObject);
			}
		}

		//TODO I need to be able to run a command, wait for it to finish, then run the next.
		public void RunCommand(int cardIndex, BotMovement bot){
			cards[cardIndex].AttachAbilityTo(bot.gameObject);
			cards[cardIndex].Use(bot);
		}
	}
}