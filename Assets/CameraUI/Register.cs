using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour {

	[SerializeField] BotControl player;

	TurnHandler turnHandler;
	Card[] cards;

	void Start(){
		turnHandler = FindObjectOfType<TurnHandler>();
	}

	public void ProcessRegister()
    {
		cards = GetComponentsInChildren<Card>();
		print(turnHandler.NumberOfTurnsPerRound + " / " + cards.Length);
		if (turnHandler.NumberOfTurnsPerRound == cards.Length){
			player.ProcessNextRound();
        	HandleCardCommandsInRegister();
		}else{
			//TODO Show in UI
			//TODO Don't allow too many in register
			print("You don't have enough cards in the register.");
		}
        
    }

    private void HandleCardCommandsInRegister()
    {
        foreach (Card card in cards)
        {
            player.AddCardToProcessor(card.GetCardConfig);
			//TODO
            //Flag player as "ready" -> Start actions after all players submit ready
        }
    }
}
