using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo.Board;
using System;

public class Score : MonoBehaviour {
	List<Checkpoint> collectedCheckpoints = new List<Checkpoint>();
	
	public void CollectCheckpoint(Checkpoint checkpoint){
		if (!collectedCheckpoints.Contains(checkpoint)){
			collectedCheckpoints.Add(checkpoint);
		}
	}

	//TODO make this not terrible
    public bool AreAllCheckpointsCollected()
    {
        var allCheckpoints = FindObjectsOfType<Checkpoint>();
		if (allCheckpoints.Length == collectedCheckpoints.Count){
			return true;
		}
		return false;
    }
}
