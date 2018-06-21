using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour {
	[SerializeField] int currentTurnNumber = 0;


	List<BotControl> players;
	int numPlayers;

	public int CurrentTurnNumber { get { return currentTurnNumber; }}

	// Use this for initialization
	void Start () {
		players = new List<BotControl>(FindObjectsOfType<BotControl>());
		numPlayers = players.Count;
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
        currentTurnNumber++;
		players = new List<BotControl>(FindObjectsOfType<BotControl>());
		print("NEXT TURN! " + currentTurnNumber);
    }
}
