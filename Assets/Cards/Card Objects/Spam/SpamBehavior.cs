using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

namespace Robo.Cards{
	public class SpamBehavior : CardBehavior {

		Vector2Int direction;
		Vector2Int movePosition;
		
		//Draw a random card, play it, then discard it
		public override void Use(BotMovement bot){
			Deck deck = bot.GetComponent<Deck>();
			List<CardConfig> cards = deck.DrawCards(1);
			if (cards.Count == 1){
				CardConfig card = cards[0];
				card.AttachAbilityTo(bot.gameObject);
				card.Use(bot);
				if (!card.DestroyCardAfterPlaying){
					deck.DiscardCard(card);
				}
				Destroy(this);
			}
			
		}
		
	}
}