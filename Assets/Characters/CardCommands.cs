using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCommands : MonoBehaviour {

	[SerializeField] CardConfig[] cards;
	public bool commandRunning = false;
	TurnHandler turnHandler;

	public CardConfig[] getCards(){
		return cards;
	}

	void Awake() {
		turnHandler = FindObjectOfType<TurnHandler>();
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
	public IEnumerator RunCommands(GameObject target = null){
		for (int cardIndex = 0; cardIndex < cards.Length; cardIndex++){
			cards[cardIndex].Use(target);
			yield return new WaitForSecondsRealtime(.5f);
		}
	}


	
}
