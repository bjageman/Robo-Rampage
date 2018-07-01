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
	List<BotMovement> players;

	public int CurrentTurn { get { return currentTurn; }}
	public int CurrentRound { get { return currentRound; }}
	public int NumberOfCardsPlayedPerRound { get { return numberOfTurnsPerRound; }}

	bool obstaclesActivated = false;

	public delegate void ActivateObstacles();
	public event ActivateObstacles onActivateObstacles;

	void Awake() {
		//TODO Enforce singleton
	}

	void Start ()
    {
        currentTurn = startingTurn;
		players = new List<BotMovement>();
    }

	public void AddPlayerToQueue(BotMovement newPlayer){
		players.Add(newPlayer);
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
		if (players.Count > 0){
			return players[0];
		}
		return null;
	}

	public void submitTurn(BotMovement player){
		for (int i = players.Count - 1; i >= 0; i--){
			if (player.gameObject.name == players[i].gameObject.name){
				players.Remove(players[i]);
			}
		}
		if (players.Count == 0){
			if (obstaclesActivated){
				obstaclesActivated = false;
				nextTurn();
			}else{
				onActivateObstacles();
				AddPlayersToQueue();
				obstaclesActivated = true;
			}
			
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
		print("Turn COMPLETE");
    }
}
