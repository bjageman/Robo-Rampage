using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCommands : MonoBehaviour {

	[SerializeField] CardConfig[] cards;
	public bool commandRunning = false;
	
	public CardConfig[] getCards(){
		return cards;
	}

	void Start() {
		//TODO  Get Audio Source
		AttachInitialCommands();
	}

	//TODO Probably won't need to attach
	private void AttachInitialCommands(){
		for (int cardIndex = 0; cardIndex < cards.Length; cardIndex++){
			cards[cardIndex].AttachCardTo(gameObject);
		}
	}

	//TODO I need to be able to run a command, wait for it to finish, then run the next.
	public void RunCommand(int cardIndex, BotControl bot){
		print("Running "+ cards[cardIndex].name + " " + bot.gameObject.name);
		cards[cardIndex].Use(bot);
	}


	
}
