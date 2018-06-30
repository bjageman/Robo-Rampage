using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Robo.CameraUI; //TODO Consider refactor

namespace Robo.Cards{
	public class Hand : MonoBehaviour {
		[SerializeField] Deck deck; 
		[SerializeField] Card cardPrefab;
		[SerializeField] Slot slotPrefab;
		[SerializeField] int maxHandSize = 9;

		public void Start(){
			DrawHandToFull();
		}

		public void DrawHandToFull()
		{
			var currentCardsInHand = GetComponentsInChildren<Card>();
			int numCardsToDraw = maxHandSize - currentCardsInHand.Length;
			if (numCardsToDraw < 0){ numCardsToDraw = 0; }
			List<CardConfig> cards = deck.DrawCards(numCardsToDraw); //TODO Only draw the necessary number of cards
			foreach (CardConfig card in cards){
				CreateCard(card);
			}
		}

		private void CreateCard(CardConfig cardConfig)
		{
			Slot slot = FindOrCreateEmptySlot();
			Card card = Instantiate(cardPrefab, slot.transform);
			card.GetComponent<Card>().SetCardConfig = cardConfig;
			card.GetComponentInChildren<Text>().text = cardConfig.CardName;
			card.SetSprite(cardConfig.CardSprite);
		}

        private Slot FindOrCreateEmptySlot()
        {
            Slot[] slotsInHand = GetComponentsInChildren<Slot>();
			foreach(Slot slot in slotsInHand){
				if(slot.GetComponentInChildren<Card>() == null){
					return slot;
				}
			}
			return Instantiate(slotPrefab, transform);
        }
    }

}