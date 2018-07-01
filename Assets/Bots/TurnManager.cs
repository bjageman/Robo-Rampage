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
	bool obstaclesActivated = false;


	public int CurrentTurn { get { return currentTurn; }}
	public int CurrentRound { get { return currentRound; }}
	public int NumberOfCardsPlayedPerRound { get { return numberOfTurnsPerRound; }}
	public bool ObstaclesActivated { get { return obstaclesActivated; }}



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
		print(players.Count);
		for (int i = players.Count - 1; i >= 0; i--){
			if (GameObject.ReferenceEquals(player.gameObject, players[i].gameObject)){
				players.Remove(players[i]);
			}
		}
		if (players.Count == 0){
			if (obstaclesActivated){
				obstaclesActivated = false;
				nextTurn();
			}else{
				obstaclesActivated = true;
				onActivateObstacles();
			}
			
		}
	}

	//TODO Set up an observer
    private void nextTurn()
    {
		if (currentTurn == numberOfTurnsPerRound){
			currentTurn = startingTurn;
			currentRound++;
		}else{
			currentTurn++;
	        AddPlayersToQueue();
		}
    }
}
