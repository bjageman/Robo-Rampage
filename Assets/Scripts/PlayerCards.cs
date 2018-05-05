using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour {

	[SerializeField] int MoveOne = 15;
	[SerializeField] int MoveTwo = 10;
	[SerializeField] int MoveThree = 5;
	[SerializeField] int RotateLeft = 15;
	[SerializeField] int RotateRight = 15;
	[SerializeField] int UTurn = 7;

	List<Command> deck;
	List<Command> hand;
	List<Command> discardPile;

	// Use this for initialization
	public void Start () {
		ProcessDeck ();
		DrawHand (5);
	}
		
	public List<Command> GetDeck(){
		return deck;
	}

	public List<Command> GetHand(){
		return hand;
	}

	public void DrawHand(int numCards){
		hand = new List<Command> ();
		for (int i = 0; i < numCards; i++) {
			Command temp = deck [0];
			hand.Add (temp);
			deck.RemoveAt (0);
		}
	}

	public void MockHand(){
		hand = new List<Command> ();
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Rotate", 1));
		hand.Add (new Command ("Move", 1));

		hand.Add (new Command ("Rotate", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Rotate", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Rotate", 2));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Rotate", -1));
		hand.Add (new Command ("Move", 1));
		hand.Add (new Command ("Move", 1));
	}

	public void ProcessDeck ()
	{
		CreateDeck ();
		ShuffleDeck (deck);
	}

	public void AddCardToDeck(Command card){
		deck.Add (card);
	}

	public void ShuffleDeck(List<Command> deck){
		for (int i = 0; i < deck.Count; i++) {
			Command temp = deck [i];
			int randomIndex = Random.Range (i, deck.Count);
			deck [i] = deck [randomIndex];
			deck [randomIndex] = temp;
		}
	}


	void CreateDeck(){
		deck = new List<Command> ();
		for (int i = 0; i < MoveOne; i++) {
			AddCardToDeck(new Command("Move", 1));
		}
		for (int i = 0; i < MoveTwo; i++) {
			AddCardToDeck(new Command("Move", 2));
		}
		for (int i = 0; i < MoveThree; i++) {
			AddCardToDeck(new Command("Move", 3));
		}
		for (int i = 0; i < RotateLeft; i++) {
			AddCardToDeck(new Command("Rotate", -1));
		}
		for (int i = 0; i < RotateRight; i++) {
			AddCardToDeck(new Command("Rotate", 1));
		}
		for (int i = 0; i < UTurn; i++) {
			AddCardToDeck(new Command("Rotate", 2));
		}

	}
}
