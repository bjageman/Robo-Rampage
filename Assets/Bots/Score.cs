using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Board;
using System;

public class Score : MonoBehaviour {
	int currentCheckpoint = 0;
	int currentEnergy = 5;

	public int CurrentCheckpoint { get { return currentCheckpoint; }}

	public void AddEnergy(int amount){
		currentEnergy += amount;
	}

	public void CollectCheckpoint(int checkpointOrder){
		if (currentCheckpoint == checkpointOrder - 1){
			currentCheckpoint++;
			print("Collected Flag " + checkpointOrder);
		}
	}

}
