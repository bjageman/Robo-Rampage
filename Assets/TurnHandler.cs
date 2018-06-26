using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour {

	[SerializeField] int numberOfTurnsPerRound = 5;

	int startingTurn = 1;
	int currentTurn;
	int currentRound = 1;
	//TODO Change to Queue
	List<BotControl> players;

	public int CurrentTurn { get { return currentTurn; }}
	public int CurrentRound { get { return currentRound; }}
	public int NumberOfTurnsPerRound { get { return numberOfTurnsPerRound; }}

	void Start () {
		currentTurn = startingTurn;
		players = new List<BotControl>(FindObjectsOfType<BotControl>());
	}
	
	public BotControl getActiveTurn(){
		return players[0];
	}

	public void submitTurn(BotControl player){
		for (int i = players.Count - 1; i >= 0; i--){
			if (player.gameObject.name == players[i].gameObject.name){
				players.Remove(players[i]);
			}
		}
		if (players.Count == 0){
			nextTurn();
		}
	}

    private void nextTurn()
    {
		if (currentTurn == numberOfTurnsPerRound){
			currentTurn = startingTurn;
			currentRound++;
		}else{
	        currentTurn++;
		}
		players = new List<BotControl>(FindObjectsOfType<BotControl>());
    }
}
