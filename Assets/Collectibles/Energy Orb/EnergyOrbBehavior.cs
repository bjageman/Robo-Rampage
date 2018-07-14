using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrbBehavior : CollectibleBehavior {

	public override void Use(GameObject collector){
		Score score = collector.GetComponent<Score>();
		if (score != null){
			score.AddEnergy((config as EnergyOrbCollectible).EnergyAmount);
		}
	}

}
