using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : CardBehavior {

	BotControl bot = null;
	Vector2Int direction;
	Vector2Int movePosition;
	
	public void Start(){
		bot = gameObject.GetComponent<BotControl>();
	}

	public override void Use(GameObject target){
		bot = gameObject.GetComponent<BotControl>();
		print(bot.gameObject.name + " Moving " + (config as MoveConfig).MoveSpaces);
		direction = bot.GetFacingDirection();
		var moveDirectionPower = new Vector2Int(direction.x * (config as MoveConfig).MoveSpaces, direction.y * (config as MoveConfig).MoveSpaces);
		movePosition = new Vector2Int(
			Mathf.RoundToInt(transform.position.x + moveDirectionPower.x), 
			Mathf.RoundToInt(transform.position.z + moveDirectionPower.y)
		);
		bot.SetDestinationWaypoint(movePosition);
	}
	
}
