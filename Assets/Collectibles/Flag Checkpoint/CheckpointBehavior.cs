using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehavior : CollectibleBehavior {

	public override void Use(GameObject collector){
		Score score = collector.GetComponent<Score>();
		if (score != null){
			score.CollectCheckpoint((config as CheckpointCollectible).FlagOrder);
		}
	}

}
