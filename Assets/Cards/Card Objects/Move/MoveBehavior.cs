using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : CardBehavior {

	Vector2Int direction;
	Vector2Int movePosition;
	
	public override void Use(BotControl bot){
		print(bot.gameObject.name + " Moving " + (config as MoveConfig).MoveSpaces);
		direction = bot.GetFacingDirection();
		var moveDirectionPower = new Vector2Int(direction.x * (config as MoveConfig).MoveSpaces, direction.y * (config as MoveConfig).MoveSpaces);
		movePosition = new Vector2Int(
			Mathf.RoundToInt(bot.transform.position.x + moveDirectionPower.x), 
			Mathf.RoundToInt(bot.transform.position.z + moveDirectionPower.y)
		);
		bot.SetDestinationWaypoint(movePosition);
	}
	
}
