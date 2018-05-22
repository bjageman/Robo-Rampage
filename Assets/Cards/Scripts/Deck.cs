using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	[SerializeField] CardDisplay cardPrefab;
	[SerializeField] Card[] cards;

	public void CreateDeck(){
		foreach (Card card in cards ){
			var newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);
			newCard.card = card;
			newCard.transform.SetParent(transform);
		}
		
	}
}
