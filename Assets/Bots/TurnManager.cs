using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Bots;

//TODO Maybe combine Register and TurnHandler
public class TurnManager : MonoBehaviour {

	[SerializeField] int numberOfTurnsPerRound = 5;
	[SerializeField] int checkpointsNeededToWin = 2;

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

	public delegate void ActivateCollectibles();
	public event ActivateObstacles onActivateCollectibles;

	public delegate void FireLasers();
	public event FireLasers onFireLasers;

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

	public void RemovePlayerFromQueue(BotMovement player){
		players.Remove(player);
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

	public List<BotMovement> GetPlayersInQueue(){
		return players;
	}

    public BotMovement getActiveTurn(){
		if (players.Count > 0){
			return players[0];
		}
		return null;
	}

	public void submitTurn(BotMovement player){
		for (int i = players.Count - 1; i >= 0; i--){
			if (players[i] != null && GameObject.ReferenceEquals(player.gameObject, players[i].gameObject)){
				players.Remove(players[i]);
			}
		}
		if (players.Count == 0){
			if (obstaclesActivated){
				onActivateCollectibles(); //TODO Make sure these two don't run simultaneously (in case there is a push feature for the laser)
				onFireLasers();
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
		CheckWinConditions();
		if (currentTurn == numberOfTurnsPerRound){
			currentTurn = startingTurn;
			currentRound++;
		}else{
			currentTurn++;
	        AddPlayersToQueue();
		}
    }

    private void CheckWinConditions()
    {
        BotMovement[] existingPlayers = FindObjectsOfType<BotMovement>();
		if (existingPlayers.Length <= 0){ print("Everyone died! Stalemate."); }
		if (existingPlayers.Length == 1){ print(existingPlayers[0] + " wins!"); }
		foreach(BotMovement player in existingPlayers){
			if(player.GetComponent<Score>().CurrentCheckpoint >= checkpointsNeededToWin){
				print(player + " wins! Got final checkpoint!");
			}
		}
    }
}
