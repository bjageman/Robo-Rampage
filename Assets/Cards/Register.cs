using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

//TODO Maybe combine Register and TurnHandler
namespace Robo.Cards{
	public class Register : MonoBehaviour {

		[SerializeField] BotMovement bot;
		[SerializeField] Deck deck; //TODO This should only be in one place...
		[SerializeField] Hand hand; //TODO Don't do this either. Probably use an observer

		TurnManager turnHandler;
		Card[] cards;

		void Start(){
			turnHandler = FindObjectOfType<TurnManager>();
		}

		//Run when clicked "SUBMIT"
		public void ProcessRegister()
		{
			cards = GetComponentsInChildren<Card>();
			if (turnHandler.NumberOfCardsPlayedPerRound == cards.Length){
				bot.ProcessNextRound();
				HandleCardCommandsInRegister();
				hand.DrawHandToFull();
			}else{
				//TODO Show in UI
				//TODO Don't allow too many in register
				print("You don't have enough cards in the register. You have " + cards.Length);
			}
			
		}

		private void HandleCardCommandsInRegister()
		{
			foreach (Card card in cards)
			{
				bot.AddCardToProcessor(card.GetCardConfig);
				if (!card.GetCardConfig.DestroyCardAfterPlaying){
					deck.DiscardCard(card.GetCardConfig);
				}
				turnHandler.AddPlayerToQueue(bot);
				Destroy(card.gameObject);
			}
		}
	}
}