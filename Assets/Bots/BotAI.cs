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
		}

        //TODO rework later
        public void DrawAndPlayAllCards()
        {
            bot.cards = deck.DrawCards(turnManager.NumberOfCardsPlayedPerRound);
        }
    }
}