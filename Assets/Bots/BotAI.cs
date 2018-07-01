using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Cards;

namespace Robo.Bots
{
    //TODO Bots are not performing second turn
    public class BotAI : MonoBehaviour
    {
        Deck deck;
        int currentRound = 1;

        Register[] players; 
        BotMovement bot;
        TurnManager turnManager;

        void Start(){
            turnManager = FindObjectOfType<TurnManager>();
            players = FindObjectsOfType<Register>();
            bot = GetComponent<BotMovement>();
            deck = GetComponent<Deck>();
        }

        void Update(){
            if (currentRound == turnManager.CurrentRound){
                if (turnManager.GetPlayersInQueue().Count == players.Length){
                    bot.ProcessNextRound();
                    turnManager.AddPlayerToQueue(bot);
                    currentRound++;	
                    DrawAndPlayAllCards();
                }
            }
        }

        //TODO rework AI later
        public void DrawAndPlayAllCards()
        {
            List<Robo.Cards.CardConfig> cards = deck.DrawCards(turnManager.NumberOfCardsPlayedPerRound);
            foreach (CardConfig card in cards)
			{
				bot.AddCardToProcessor(card);
				if (!card.DestroyCardAfterPlaying){
					deck.DiscardCard(card);
				}
			}
        }
    }
}