using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : CardBehavior {

	BotControl bot = null;
	Vector2Int direction;
	Vector2Int movePosition;
	
	void Awake () {
		bot = GetComponent<BotControl>();
	}
	
	public override void Use(GameObject target){
		direction = bot.GetFacingDirection();
		var moveDirectionPower = new Vector2Int(direction.x * (config as MoveConfig).MoveSpaces, direction.y * 3);
		movePosition = new Vector2Int(
			Mathf.RoundToInt(transform.position.x + moveDirectionPower.x), 
			Mathf.RoundToInt(transform.position.z + moveDirectionPower.y)
		);
		bot.SetDestinationWaypoint(movePosition);
	}
	
}
