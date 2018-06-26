using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Robo.CameraUI; //TODO Consider refactor

namespace Robo.Commands{
	public class Hand : MonoBehaviour {
		[SerializeField] CardConfig[] cardConfigs; //TODO Remove and replace with a deck
		[SerializeField] Card cardPrefab;
		[SerializeField] Slot slotPrefab;


		public void Start(){
			CreateHand();
		}

		//TODO This should draw from a shuffled deck
		private void CreateHand()
		{
			foreach (CardConfig cardConfig in cardConfigs){
				CreateCard(cardConfig);
			}
		}

		private void CreateCard(CardConfig cardConfig)
		{
			var slot = Instantiate(slotPrefab, transform);
			var card = Instantiate(cardPrefab, slot.transform);
			card.GetComponent<Card>().SetCardConfig = cardConfig;
			card.GetComponentInChildren<Text>().text = cardConfig.CardName;
			card.SetSprite(cardConfig.CardSprite);
		}
	}

}