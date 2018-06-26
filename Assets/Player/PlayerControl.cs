using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Robo.Board;

namespace Robo.Commands{
public class PlayerControl : MonoBehaviour {	
	[SerializeField] int startingTurn = 1;

	BotMovement playerMovement;
	MovePipeline movePipeline;
	TurnHandler turnHandler;

	Rigidbody rigidBody;
	int playerTurn; //0 because the game shouldn't start until after processing the register
	int cardIndex = 0;
	List<CardConfig> cards;

	void Start(){
		
		turnHandler = FindObjectOfType<TurnHandler>();
		playerMovement = GetComponent<BotMovement>();
		movePipeline = GetComponent<MovePipeline>();
		playerTurn = startingTurn - 1;
		cards = movePipeline.getCards();
	}

	void Update(){
		bool cardsStillLeft = (cards.Count > cardIndex);
		bool isCurrentBotsTurn = (playerMovement == turnHandler.getActiveTurn());
		bool playerHasNotTakenTurn = (playerTurn == turnHandler.CurrentTurn);
		//print(cardsStillLeft.ToString() + " / " + isCurrentBotsTurn.ToString() + "/ " + playerHasNotTakenTurn.ToString());
		if ( playerHasNotTakenTurn && isCurrentBotsTurn && cardsStillLeft)
        {
			print("running commands");
            movePipeline.RunCommand(cardIndex, playerMovement);
            StartCoroutine(playerMovement.HandleMovement());
			cardIndex++;
			playerTurn++;
        }
    }

	public void ProcessNextRound(){
		playerTurn = startingTurn;
		cardIndex = 0;
		ClearProcessor();
		//TODO Add new round as well?
	}
	

	public void ClearProcessor(){
		cards.Clear();
	}

	public void AddCardToProcessor(CardConfig card){
		cards.Add(card);
	}

	
}

}