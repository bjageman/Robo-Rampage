using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : CardBehavior {

	Vector2Int direction;
	Vector2Int movePosition;

	public override void Use(BotControl bot){
		bot.RotateBot((config as RotateConfig).NumRotations);
		Destroy(this); //TODO Only destroys the latest CardBehavior to be initialized
	}
	
}
