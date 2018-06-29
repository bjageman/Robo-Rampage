using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robo.Commands{
	public class Deck : MonoBehaviour {
		[SerializeField] List<CardConfig> cardsInDeck;
		
		List<CardConfig> cardsInDiscard;

		// Use this for initialization
		void Start () {
			cardsInDiscard = new List<CardConfig>();
		}
		
		public List<CardConfig> DrawCards(int numCardsToDraw){
			List<CardConfig> cardsDrawn = new List<CardConfig>();
			for(int i = 0; i < numCardsToDraw; i++){
				if (cardsInDeck.Count == 0){
					ShuffleDiscardInToDeck();
					if (cardsInDeck.Count == 0){ break; } //Break if there are absolutely 0 cards left in the deck
				}
				int cardPosition = UnityEngine.Random.Range(0, cardsInDeck.Count - 1);
				cardsDrawn.Add(cardsInDeck[cardPosition]);
				cardsInDeck.RemoveAt(cardPosition);
			}
			return cardsDrawn;
		}

		public void DiscardCard(CardConfig card){
			cardsInDiscard.Add(card);
		}

		//TODO
        private void ShuffleDiscardInToDeck()
        {
            cardsInDeck.AddRange(cardsInDiscard);
			cardsInDiscard = new List<CardConfig>();
        }
    }
}