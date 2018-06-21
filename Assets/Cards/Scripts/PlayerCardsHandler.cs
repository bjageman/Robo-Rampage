using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardsHandler : MonoBehaviour {

	[SerializeField] Hand hand;

	System.Random rnd = new System.Random();

	// Use this for initialization
	public void Awake () {
		if (FindObjectsOfType<PlayerCardsHandler>().Length > 1){ Destroy(gameObject); }
	}

	public void Start(){
		//deck.CreateDeck();	
		// DrawHand ();
	}

	// public void DrawHand(){
	// 	var slots = hand.GetComponentsInChildren<Slot>();
	// 	foreach (Slot slot in slots){
	// 		var cards = deck.GetComponentsInChildren<CardDisplay>();
	// 		var card = cards[rnd.Next(0, cards.Length)];
	// 		if (!slot.item){
	// 			card.transform.SetParent(slot.transform);
	// 		}
	// 	}
	// }

	//TODO Create the deck on the fly for each player

}
