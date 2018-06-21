using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : CardBehavior {

	Vector2Int direction;
	Vector2Int movePosition;

	public override void Use(BotControl bot){
		print(bot.gameObject.name + " Rotate "+ (config as RotateConfig).NumRotations);
		bot.RotateBot((config as RotateConfig).NumRotations);
	}
	
}
