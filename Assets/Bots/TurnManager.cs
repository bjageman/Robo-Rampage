using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

//TODO Maybe combine Register and TurnHandler
public class TurnManager : MonoBehaviour {

	[SerializeField] int numberOfTurnsPerRound = 5;

	int startingTurn = 1;
	int currentTurn;
	int currentRound = 1;
	//TODO Change to Queue
	List<BotMovement> players;

	public int CurrentTurn { get { return currentTurn; }}
	public int CurrentRound { get { return currentRound; }}
	public int NumberOfCardsPlayedPerRound { get { return numberOfTurnsPerRound; }}

	public delegate void OnTurnEnd();
	public event OnTurnEnd onTurnEnd;

	void Start ()
    {
        AddPlayersToQueue();
        currentTurn = startingTurn;
    }

	//TODO Make this more elegant
    private void AddPlayersToQueue()
    {
		//First add human players, then AI
		players = new List<BotMovement>();
		BotMovement[] playersToSort = FindObjectsOfType<BotMovement>();
		foreach(BotMovement playerToSort in playersToSort){
			if(playerToSort.GetComponent<BotAI>() == null){
				players.Add(playerToSort);
			}
		}
		foreach(BotMovement playerToSort in playersToSort){
			if(playerToSort.GetComponent<BotAI>()){
				players.Add(playerToSort);
			}
		}
    }

    public BotMovement getActiveTurn(){
		return players[0];
	}

	public void submitTurn(BotMovement player){
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
		onTurnEnd();
		AddPlayersToQueue();
    }
}
