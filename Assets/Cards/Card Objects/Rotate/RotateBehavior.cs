using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : CardBehavior {

	BotControl bot = null;
	Vector2Int direction;
	Vector2Int movePosition;
	
	public void Start(){
		bot = gameObject.GetComponent<BotControl>();
	}

	public override void Use(GameObject target){
		print(bot.gameObject.name + " Rotate "+ (config as RotateConfig).NumRotations);
		bot.RotateBot((config as RotateConfig).NumRotations);
	}
	
}
