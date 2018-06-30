using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Cards;

namespace Robo.Bots
{
    public class BotAI : MonoBehaviour
    {
        Deck deck;

        int currentRound = 1;

        BotMovement bot;
        TurnManager turnManager;
        List<CardConfig> cards;

        void Start(){
            turnManager = FindObjectOfType<TurnManager>();
            bot = GetComponent<BotMovement>();
            deck = GetComponent<Deck>();
        }

        void Update(){
            if (currentRound == turnManager.CurrentRound){
                currentRound++;	
                ProcessAIMoves();
            }
        }

        public void ProcessAIMoves()
		{
			DrawAndPlayAllCards();
			bot.ProcessNextRound();
            HandleCardCommandsInRegister();	       	
		}

		private void HandleCardCommandsInRegister()
		{
			foreach (CardConfig card in cards)
			{
				bot.AddCardToProcessor(card);
				deck.DiscardCard(card);
			}
		}

        //TODO rework later
        public void DrawAndPlayAllCards()
        {
            cards = deck.DrawCards(turnManager.NumberOfCardsPlayedPerRound);
        }
    }
}